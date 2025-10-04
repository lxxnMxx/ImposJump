using UnityEngine;

public class ParticleHandler : Singleton<ParticleHandler>
{
    private ParticleSystem _particleSystem;

    public void SpawnParticles(ParticleSystem particleSystem, Vector3 position, Quaternion rotation)
    {
        _particleSystem = Instantiate(particleSystem, position, rotation);
        _particleSystem.gameObject.TryGetComponent(out ParticleSystem component);
        Destroy(_particleSystem.gameObject, component.main.duration + component.startLifetime);
    }
}