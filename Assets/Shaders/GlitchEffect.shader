Shader "Custom/GlitchEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlitchIntensity ("Glitch Intensity", Range(0, 1)) = 0
        _TimeScale ("Time Scale", Float) = 1
        _FlipY ("Flip Y", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "Glitch"
            ZTest Always Cull Off ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float _GlitchIntensity;
            float _TimeScale;

            struct Attributes {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = v.uv;
                return o;
            }

            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            float _FlipY;

            half4 frag(Varyings i) : SV_Target
            {
                float2 uv = i.uv;
                uv.y = lerp(uv.y, 1.0 - uv.y, _FlipY); // Flip Y if _FlipY == 1
                float t = _Time.y * _TimeScale;

                float sliceY = floor(uv.y * 40.0);
                float glitchLine = rand(float2(sliceY, t)) * _GlitchIntensity;

                if (glitchLine > 0.7)
                {
                    uv.x += glitchLine * 0.2;
                }

                if (rand(float2(t * 10.0, uv.y)) < _GlitchIntensity * 0.3)
                {
                    uv.y += sin(t * 50.0 + uv.x * 30.0) * 0.04 * _GlitchIntensity;
                }

                float offset = 0.01 * _GlitchIntensity;
                float r = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(offset, 0)).r;
                float g = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv).g;
                float b = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv - float2(offset, 0)).b;

                return float4(r, g, b, 1);
            }
            ENDHLSL
        }
    }
}
