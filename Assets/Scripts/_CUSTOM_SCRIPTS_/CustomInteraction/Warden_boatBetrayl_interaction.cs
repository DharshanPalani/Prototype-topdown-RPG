using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interact;


public enum betraylState {
    NOT_TALKED,
    TALKED
}

public class Warden_boatBetrayl_interaction : MonoBehaviour, IInteractible
{
    public DialogueScriptableObject betraylDialogue;

    public betraylState npc;

    public void OnInteract()
    {
        switch (npc)
        {
            case betraylState.NOT_TALKED:
                FindObjectOfType<DialogueManager>().StartDialogue(betraylDialogue, null);
                npc = betraylState.TALKED;
                break;
            case betraylState.TALKED:
                break;
        }
    }
}
