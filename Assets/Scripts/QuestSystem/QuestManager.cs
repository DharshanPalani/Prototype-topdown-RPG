using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<int, Quest> activeQuests = new Dictionary<int, Quest>();

    void Update()
    {
        List<string> questNames = new List<string>();
        foreach (var quest in activeQuests.Values)
        {
            if (!quest.isComplete)
            {
                questNames.Add(quest.questName);
            }
        }

        FindObjectOfType<QuestUI>().questUpdateUI(questNames);
    }


    public void AddProgress(int amount, int id)
    {
        if (activeQuests.TryGetValue(id, out Quest quest) && quest != null && !quest.isComplete)
        {
            quest.UpdateProgress(amount);
        }
    }

    public void AddQuest(Quest quest)
    {
        Debug.Log("Call from AddQuest");
        if (activeQuests.ContainsKey(quest.questID))
            Debug.Log("Quest already added or done");
        else
            activeQuests.Add(quest.questID, quest);
    }
}
