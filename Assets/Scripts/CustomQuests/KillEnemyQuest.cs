using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyQuest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            FindObjectOfType<QuestManager>()?.AddProgress(1,1);
        }
    }
}
