using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private string id;
    
    private bool _isCollected;
    private bool _isInCollection;
    private bool _shouldShow;


    
    private void OnEnable()
    {
        gameObject.SetActive(!LevelManager.Instance.levels[LevelManager.Instance.GetActiveLevel()].coinsCollected.TryGetValue(id, out _isCollected));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!_isCollected)
                _isCollected = true;
            
            if (LevelManager.Instance.levels[LevelManager.Instance.GetActiveLevel()].coinsCollected.ContainsKey(id))
            {
                LevelManager.Instance.levels[LevelManager.Instance.GetActiveLevel()].coinsCollected.Remove(id);
                LevelManager.Instance.levels[LevelManager.Instance.GetActiveLevel()].coinsCollected.Add(id, _isCollected);
            }
            else
            {
                LevelManager.Instance.levels[LevelManager.Instance.GetActiveLevel()].coinsCollected.Add(id, _isCollected);
            }
            
            gameObject.SetActive(false);
        }
    }
}
