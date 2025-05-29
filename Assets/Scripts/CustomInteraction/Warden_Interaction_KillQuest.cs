using System.Collections;
using System.Collections.Generic;
using interact;
using UnityEngine;

public class Warden_Interaction_KillQuest : MonoBehaviour, IInteractible
{
    public Quest killMonsterQuest;

    public void OnInteract()
    {
        FindObjectOfType<QuestManager>().AddQuest(killMonsterQuest);
    }
}
