using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class PoolingHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> pool;
    
    private GameObject _obj;

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        return FindActiveObjects(pool, position, rotation);
    }

    public void Despawn(GameObject go)
    {
        go.SetActive(false);
    }

    GameObject FindActiveObjects(List<GameObject> gameObjects, Vector3 position, Quaternion rotation)
    {
        GameObject go = null;
        foreach (var obj in gameObjects.Where(obj => obj.activeInHierarchy == false))
        {
            go = obj;
            break;
        }
        if (go)
        {
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
            return go;
        }
        Debug.LogError("NullReferenceException: Can't find an object!!!!!!!!!!!!!!!!!!!!!!!!");
        return null;
    }
}
