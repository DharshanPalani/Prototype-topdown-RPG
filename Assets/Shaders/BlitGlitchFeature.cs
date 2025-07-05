using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlitGlitchFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class GlitchSettings
    {
        public Material glitchMaterial;
        [Range(0, 1)] public float intensity = 0.5f;
        public float timeScale = 1.0f;
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRendering;
        public bool StartGlitch = false;
    }

    public GlitchSettings settings = new GlitchSettings();

    class GlitchPass : ScriptableRenderPass
    {
        private Material glitchMaterial;

        private bool startGlitch;
        private float intensity;
        private float timeScale;
        private string profilerTag = "GlitchEffect";
        private RenderTargetIdentifier source;
        private RenderTargetHandle tempTexture;

        public void Setup(Material material, float intensity, float timeScale, bool startGlitch)
        {
            this.glitchMaterial = material;
            this.intensity = intensity;
            this.timeScale = timeScale;
            this.startGlitch = startGlitch;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            tempTexture.Init("_TemporaryGlitchTex");
            cmd.GetTemporaryRT(tempTexture.id, cameraTextureDescriptor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (glitchMaterial == null || !startGlitch) return;

            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

            // Apply glitch uniforms
            glitchMaterial.SetFloat("_GlitchIntensity", intensity);
            glitchMaterial.SetFloat("_TimeScale", timeScale);

            // Flip only in Game View
            bool isGameView = !renderingData.cameraData.isSceneViewCamera;
            glitchMaterial.SetFloat("_FlipY", isGameView ? 1f : 0f);

            source = renderingData.cameraData.renderer.cameraColorTarget;

            cmd.Blit(source, tempTexture.Identifier(), glitchMaterial);
            cmd.Blit(tempTexture.Identifier(), source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }


        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (tempTexture != RenderTargetHandle.CameraTarget)
            {
                cmd.ReleaseTemporaryRT(tempTexture.id);
            }
        }
    }

    private GlitchPass glitchPass;

    public override void Create()
    {
        glitchPass = new GlitchPass();
        glitchPass.renderPassEvent = settings.renderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.glitchMaterial == null) return;

        glitchPass.Setup(settings.glitchMaterial, settings.intensity, settings.timeScale, settings.StartGlitch);
        renderer.EnqueuePass(glitchPass);
    }
}
