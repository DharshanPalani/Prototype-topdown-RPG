using UnityEngine;

public enum boatQuestState
{
    NOT_COLLECTED_ITEMS,
    NOT_FIXED_BOAT,
    FIXED_BOAT,
    FINISHED_BOAT_QUEST
}

public class BoatFixQuest : MonoBehaviour
{
    public int questID;
    public boatQuestState state;
    public KeyCode fixKey = KeyCode.E;
    public float holdTime = 10f;

    private float currentHoldTime = 0f;
    private bool playerInRange = false;

    void Update()
    {
        if (state == boatQuestState.FINISHED_BOAT_QUEST) return;

        if (state == boatQuestState.FIXED_BOAT)
        {
            state = boatQuestState.FINISHED_BOAT_QUEST;
            FindObjectOfType<QuestManager>().AddProgress(1, questID);
        }

        if (state != boatQuestState.NOT_FIXED_BOAT || !playerInRange) return;

        if (Input.GetKey(fixKey))
        {
            currentHoldTime += Time.deltaTime;

            if (currentHoldTime >= holdTime)
            {
                state = boatQuestState.FIXED_BOAT;
            }
        }
        else
        {
            if (currentHoldTime > 0f)
            {
                Debug.Log("Fixing interrupted. Hold again to fix.");
            }
            currentHoldTime = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            currentHoldTime = 0f;
        }
    }
}
