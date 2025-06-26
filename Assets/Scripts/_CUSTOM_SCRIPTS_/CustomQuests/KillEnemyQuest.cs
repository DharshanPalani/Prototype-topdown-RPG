using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyQuest : MonoBehaviour
{

    public int questID;

    public void updateKilledMoster()
    {
        FindObjectOfType<QuestManager>().AddProgress(questID, 1);
    }
}
