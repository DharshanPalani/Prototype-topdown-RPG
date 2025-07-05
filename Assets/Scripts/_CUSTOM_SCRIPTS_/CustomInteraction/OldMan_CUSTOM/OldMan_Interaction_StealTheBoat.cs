using System.Collections;
using interact;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OldMan_Interaction_StealTheBoat : MonoBehaviour, IInteractible
{
    public DialogueScriptableObject oldManFinalDialogue;
    private bool hadConvo;

    public float glitchMax = 1.0f;
    public float glitchDuration = 3f;
    public BlitGlitchFeature glitchFeature;
    private void OnDisable()
    {
        glitchFeature.settings.intensity = 0;
        glitchFeature.settings.StartGlitch = false;
    }

    public void OnInteract()
    {
        if (hadConvo) return;

        DialogueManager.Instance.StartDialogue(oldManFinalDialogue, () =>
        {
            hadConvo = true;
            StartCoroutine(increaseGlitchIntensity());
        });
    }

    private IEnumerator increaseGlitchIntensity()
    {
        float timer = 0f;
        float startIntensity = glitchFeature.settings.intensity;

        glitchFeature.settings.StartGlitch = true;

        while (timer < glitchDuration)
        {
            timer += Time.deltaTime;
            float t = timer / glitchDuration;
            glitchFeature.settings.intensity = Mathf.Lerp(startIntensity, glitchMax, t);
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        glitchFeature.settings.intensity = glitchMax;
    }
}
