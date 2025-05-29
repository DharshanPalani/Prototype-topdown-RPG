using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<int, Quest> activeQuests = new Dictionary<int, Quest>();

    public void AddProgress(int amount, int id)
    {
        if (activeQuests.TryGetValue(id, out Quest quest) && quest != null && !quest.isComplete)
        {
            quest.UpdateProgress(amount);
        }
    }

    public void AddQuest(Quest quest)
    {
        Debug.Log("Call from addquest");
        if (activeQuests.ContainsKey(quest.questID))
            Debug.Log("Quest alread added or done");
        else
            activeQuests.Add(quest.questID, quest);   
    }

}
