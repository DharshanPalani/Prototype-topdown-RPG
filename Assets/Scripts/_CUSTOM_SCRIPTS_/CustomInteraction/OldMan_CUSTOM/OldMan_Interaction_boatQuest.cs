using System.Collections;
using System.Collections.Generic;
using interact;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OldMan_Interaction_boat_quest : MonoBehaviour, IInteractible
{
    public Quest boatQuest;

    public OldMan_State_enum state;

    public DialogueScriptableObject QuestDialogue;
    public DialogueScriptableObject QuestGuideDialogue;
    public DialogueScriptableObject QuestAfterDialogue;

    void Update()
    {
        if (boatQuest.isComplete) state = OldMan_State_enum.TALK_THANK_FOR_QUEST;
    }

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
            case OldMan_State_enum.TALK_THANK_FOR_QUEST:
                DialogueManager.Instance.StartDialogue(QuestAfterDialogue, AfterQuestFinishAndTalkAction);
                break;

        }
    }

    public void AssignQuestOnDialogueComplete()
    {
        FindObjectOfType<QuestManager>().AddQuest(boatQuest);
    }

    private void AfterQuestFinishAndTalkAction()
    {
        Debug.Log("Do shit here after quest finish talk");
    }
}
