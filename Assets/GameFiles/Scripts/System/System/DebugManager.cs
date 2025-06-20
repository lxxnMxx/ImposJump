using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    void Update()
    {
        if (SceneHandler.Instance.IsCurrentSceneLevel() && Input.GetKeyDown(KeyCode.P))
        {
            SceneHandler.Instance.LoadLevel(SceneManager.GetActiveScene().name);
        }

        if (SceneHandler.Instance.IsCurrentSceneTutorial() && Input.GetKeyDown(KeyCode.P))
        {
            SceneHandler.Instance.LoadTutorial(SceneManager.GetActiveScene().name);
        }
    }
}
