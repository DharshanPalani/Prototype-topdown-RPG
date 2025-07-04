using System.Collections;
using System.Collections.Generic;
using interact;
using UnityEngine;

public class OldMan_Interaction_StealTheBoat : MonoBehaviour, IInteractible
{
    public DialogueScriptableObject oldManFinalDialogue;
    private bool hadConvo;

    public void OnInteract()
    {
        if (hadConvo) return;
        DialogueManager.Instance.StartDialogue(oldManFinalDialogue, () => { hadConvo = true; });
    }
}
