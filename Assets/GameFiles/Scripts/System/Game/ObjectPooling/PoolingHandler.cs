using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PoolingHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> pool;
    
    private GameObject _obj;

    // only for debugging purposes
    private void Awake()
    {
        foreach (var o in pool)
        {
            if (o.TryGetComponent(out ISpawnable spawnable))
                continue;
            Debug.LogError($"{o.name} does not implement ISpawnable interface (PoolingHandler l.20)");
        }
    }

    public async Task<GameObject> Spawn(Vector3 position, Quaternion rotation)
    {
        return FindActiveObjects(pool, position, rotation);
    }

    public void Despawn(GameObject go)
    {
        go.SetActive(false);
    }
    
    GameObject FindActiveObjects(List<GameObject> gameObjects, Vector3 position, Quaternion rotation)
    {
        GameObject go = gameObjects.FirstOrDefault(obj => !obj.active);
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
