using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public enum DecorationType
{
    Bird
}

// !!! the B or C behind Variables stands for birds or clouds !!!

//TODO: IF THERE ARE ONLY DEACTIVATED OBJECTS, THEN THE SCRIPT DOESN'T FIND ANYTHING, FIX!!!!!!
public class PoolingHandler : Singleton<PoolingHandler>
{
    private GameObject obj;
    
    public GameObject Spawn(DecorationType type, Vector3 position, Quaternion rotation)
    {
        switch (type)
        {
            case DecorationType.Bird:
                return FindActiveObjects(PoolingObjects.Instance.birdPool, position, rotation);
            default:
                Debug.LogError("NullPointerException: Can't find an object !!!!!!!!!!!!!!!!!");
                return null;
        }
    }

    public void Despawn(GameObject go)
    {
        go.SetActive(false);
    }

    GameObject FindActiveObjects(List<GameObject> gameObjects, Vector3 position, Quaternion rotation)
    {
        GameObject go = null;
        // for loop and if Statement in one expression
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
