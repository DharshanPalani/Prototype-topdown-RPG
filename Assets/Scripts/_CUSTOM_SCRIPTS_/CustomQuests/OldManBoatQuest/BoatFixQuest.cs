using UnityEngine;

public enum boatQuestState
{
    NOT_COLLECTED_ITEMS,
    COLLECTED_ITEMS,
    NOT_FIXED_BOAT,
    FIXED_BOAT
}

public class BoatFixQuest : MonoBehaviour
{
    public boatQuestState state = boatQuestState.NOT_FIXED_BOAT;
    public KeyCode fixKey = KeyCode.E;
    public float holdTime = 10f;

    private float currentHoldTime = 0f;
    private bool playerInRange = false;

    void Update()
    {
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
