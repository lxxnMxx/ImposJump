using System;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        var instance = GameObject.Find($"{gameObject.name}");
        if (instance.gameObject != gameObject)
        {
            Destroy(instance.gameObject);
        }
    }
}
