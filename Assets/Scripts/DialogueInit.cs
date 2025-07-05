using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueInit : MonoBehaviour
{
    public DialogueScriptableObject initDialogue;

    void Start()
    {
        DialogueManager.Instance.StartDialogue(initDialogue, () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }
}
