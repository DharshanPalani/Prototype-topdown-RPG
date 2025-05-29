using UnityEngine;

[System.Serializable]
public class Quest
{
    public int questID;
    public string questName;
    public string description;
    public int goalAmount;
    public int currentAmount;
    public bool isComplete;

    public delegate void QuestCompleted();
    public event QuestCompleted OnQuestCompleted;

    public void UpdateProgress(int amount)
    {
        if (isComplete) return;

        currentAmount += amount;
        if (currentAmount >= goalAmount)
        {
            currentAmount = goalAmount;
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        isComplete = true;
        Debug.Log($"Quest '{questName}' Completed!");
        OnQuestCompleted?.Invoke();
    }
}
