using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private ParticleSystem collectedParticles;
    
    private bool _isCollected;
    private bool _shouldShow;

    private ParticleSystem _ps;
    private ParticleSystem _component;
    
    private void OnEnable()
    {
        gameObject.SetActive(!LevelManager.Instance.GetActiveLevel().coinsCollected.TryGetValue(id, out _isCollected));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        PlayParticles();
        SoundManager.Instance.Play(SoundList.Player, SoundType.CoinCollected);
            
        if(!_isCollected)
            _isCollected = true;
            
        if (LevelManager.Instance.GetActiveLevel().coinsCollected.ContainsKey(id))
        {
            LevelManager.Instance.GetActiveLevel().coinsCollected.Remove(id);
            LevelManager.Instance.GetActiveLevel().coinsCollected.Add(id, _isCollected);
        }
        else
        {
            LevelManager.Instance.GetActiveLevel().coinsCollected.Add(id, _isCollected);
        }
            
        gameObject.SetActive(false);
    }

    async private void PlayParticles()
    {
        _ps = Instantiate(collectedParticles, transform.position, Quaternion.identity);
        _component = _ps.gameObject.GetComponent<ParticleSystem>();
        Destroy(_ps.gameObject,   _component.main.duration + _component.startLifetime);
    }
}
