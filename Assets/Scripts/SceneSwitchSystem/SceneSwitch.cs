using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    public GameObject triggerPoint;

    void OnEnable()
    {
        SwitchSceneEvent.OnSceneApprove += approveSceneSwitch;
    }

    void OnDisable()
    {
        SwitchSceneEvent.OnSceneApprove -= approveSceneSwitch;   
    }

    public void approveSceneSwitch()
    {
        triggerPoint.SetActive(true);
    }
}
