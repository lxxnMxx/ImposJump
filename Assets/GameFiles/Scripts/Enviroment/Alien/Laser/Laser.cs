using System;
using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    
    [Space(7)]
    
    [SerializeField] ParticleSystem impactParticle;
    private ParticleSystem _ps;
    private ParticleSystem _component;
    
    private void OnEnable()
    {
        StartCoroutine(LifeTimer());
    }
    
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Alien")) return;
        
        SoundManager.Instance.Play(SoundType.LaserImpact);
        _ps = Instantiate(impactParticle, transform.position, Quaternion.identity);
        _component = _ps.gameObject.GetComponent<ParticleSystem>();
        gameObject.SetActive(false);
        Destroy(_ps.gameObject, _component.main.duration + _component.startLifetime);
    }
}
