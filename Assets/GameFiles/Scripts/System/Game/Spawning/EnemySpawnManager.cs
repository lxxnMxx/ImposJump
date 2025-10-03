using UnityEngine;

public class EnemySpawnManager : SpawningManager
{
    protected override async void Spawn()
    {
        await Countdown(10);
        print("Hello World!");
    }

    protected override async void Despawn()
    {
        print("Hello World!");
    }
}