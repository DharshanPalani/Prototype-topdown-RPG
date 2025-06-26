using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitch : MonoBehaviour
{

    [SerializeField] private string sceneName;

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
        SceneManager.LoadScene(sceneName);
    }
}
