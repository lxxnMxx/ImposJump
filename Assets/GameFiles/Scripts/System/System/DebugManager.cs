using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneHandler.Instance.LoadLevel(SceneManager.GetActiveScene().name);
        }
    }
}
