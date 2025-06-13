using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public TMP_Text questList;

    public void questUpdateUI(List<string> questNames)
    {
        if (questList == null)
        {
            Debug.LogWarning("Quest list UI is not assigned!");
            return;
        }

        // Use StringBuilder for better performance
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        foreach (string questName in questNames)
        {
            builder.AppendLine(questName);
        }

        questList.text = builder.ToString();
    }
}
