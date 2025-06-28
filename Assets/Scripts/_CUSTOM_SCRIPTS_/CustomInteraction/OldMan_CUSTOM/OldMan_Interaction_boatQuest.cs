using System.Collections;
using System.Collections.Generic;
using interact;
using UnityEngine;

public class OldMan_Interaction_boat_quest : MonoBehaviour, IInteractible
{
    public Quest boatQuest;

    public OldMan_State_enum state;

    public DialogueScriptableObject QuestDialogue;
    public DialogueScriptableObject QuestGuideDialogue;
    public DialogueScriptableObject QuestAfterDialogue;

    public void OnInteract()
    {
        switch (state)
        {
            case OldMan_State_enum.NOT_TALKED:
                DialogueManager.Instance.StartDialogue(QuestDialogue, () =>
                {
                    AssignQuestOnDialogueComplete();
                    state = OldMan_State_enum.TALKED;
                });
                break;
            case OldMan_State_enum.TALKED:
                DialogueManager.Instance.StartDialogue(QuestGuideDialogue);
                break; 
            
        }
    }

    public void AssignQuestOnDialogueComplete()
    {
        FindObjectOfType<QuestManager>().AddQuest(boatQuest);
    }
}
