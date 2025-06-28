using System.Collections;
using System.Collections.Generic;
using interact;
using UnityEngine;

public class ToolSackWithWoods_BoatQuest : MonoBehaviour, IInteractible
{

    public BoatFixQuest boat;

    public void OnInteract()
    {
        boat.state = boatQuestState.NOT_FIXED_BOAT;
        Destroy(this.gameObject);
    }
}