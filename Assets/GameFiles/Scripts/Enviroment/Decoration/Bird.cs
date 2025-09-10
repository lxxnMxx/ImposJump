using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bird : MonoBehaviour
{
    [Tooltip("X is the minimum and y the maximum.")]
    [SerializeField] private Vector2 randomSizeRange;
    
    private void OnEnable()
    {
        var rnd = (float)Math.Round(Random.Range(randomSizeRange.x, randomSizeRange.y), 3);
        transform.localScale = new Vector3(rnd, rnd, rnd);
    }
}
