using System.Collections;
using System.Collections.Generic;
using interact;
using UnityEngine;

public class KillEnemyEntity : MonoBehaviour, IInteractible
{
    public void OnInteract()
    {
        FindObjectOfType<KillEnemyQuest>().updateKilledMoster();
    }
}
