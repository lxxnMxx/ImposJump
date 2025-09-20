using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    GameObject obj = new()
                    {
                        name = typeof(T).Name + "Singleton"
                    };
                    obj.AddComponent<Singleton<T>>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this as T;
    }
}
