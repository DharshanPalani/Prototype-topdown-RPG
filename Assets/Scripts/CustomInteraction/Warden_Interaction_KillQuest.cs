using System.Collections;
using System.Collections.Generic;
using interact;
using UnityEngine;


public enum npcState
{
    NOT_TALKED,
    TALKED,
    COMPLETE_QUEST_TALK,
    COMPLETE_TALKED
}
public class Warden_Interaction_KillQuest : MonoBehaviour, IInteractible
{
    public Quest killMonsterQuest;
    public DialogueScriptableObject dialogue;
    public DialogueScriptableObject completeQuestDialogue;

    public npcState npc;


    void Update()
    {
        if (killMonsterQuest.isComplete)
        {
            npc = npcState.COMPLETE_QUEST_TALK;
        }
    }

    public void OnInteract()
    {
        switch (npc)
        {
            case npcState.NOT_TALKED:
                npc = npcState.TALKED;
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, OnDialogueEnd);
                break;
            case npcState.TALKED:
                break;
            case npcState.COMPLETE_QUEST_TALK:
                FindObjectOfType<DialogueManager>().StartDialogue(completeQuestDialogue, null);
                npc = npcState.COMPLETE_TALKED;
                break;
            case npcState.COMPLETE_TALKED:
                break;
        }

    }

    public void OnDialogueEnd()
    {
        FindObjectOfType<QuestManager>().AddQuest(killMonsterQuest);
    }
}
