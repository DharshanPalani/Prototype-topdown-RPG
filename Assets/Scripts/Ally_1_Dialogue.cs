using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ally_1_Dialogue : MonoBehaviour
{
    public DialogueScriptableObject alleyDialogue;

    public Quest killNinjaQuest;
    public Quest killViperQuest;

    public float glitchMax = 1.0f;
    public float glitchDuration = 3f;
    public BlitGlitchFeature glitchFeature;

    void Start()
    {
        DialogueManager.Instance.StartDialogue(alleyDialogue, () =>
        {
            FindObjectOfType<QuestManager>().AddQuest(killNinjaQuest);
            FindObjectOfType<QuestManager>().AddQuest(killViperQuest);
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
