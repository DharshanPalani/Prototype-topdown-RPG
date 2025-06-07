using System.Collections;
using System.Collections.Generic;
using interact;
using UnityEngine;

public class Warden_Interaction_KillQuest : MonoBehaviour, IInteractible
{
    public Quest killMonsterQuest;
    public DialogueScriptableObject dialogue;

    public void OnInteract()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, OnDialogueEnd);
    }

    public void OnDialogueEnd() {
        FindObjectOfType<QuestManager>().AddQuest(killMonsterQuest);
    }
}
