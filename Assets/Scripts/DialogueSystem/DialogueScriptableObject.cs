using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 5)]
public class DialogueScriptableObject : ScriptableObject
{
    public string[] lines;
}
