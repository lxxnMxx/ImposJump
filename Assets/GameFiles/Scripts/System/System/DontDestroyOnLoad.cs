using System;
using UnityEngine;

public class DontDestroyOnLoad<T> : MonoBehaviour where T : Component
{
    public static T instance;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}
