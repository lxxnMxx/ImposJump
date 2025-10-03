using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SpawningManager : MonoBehaviour
{
    protected CancellationTokenSource CancelTokenSource = new CancellationTokenSource();
    
    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += LevelStart;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= LevelStart;
    }

    private void LevelStart()
    {
        if(CancelTokenSource != null)
            CancelTokenSource.Dispose();
        CancelTokenSource = new CancellationTokenSource();
        Spawn();
    }
    
    protected abstract void Spawn();
    protected abstract void Despawn();

    /// <summary>
    /// Just a countdown, that counts down the time.
    /// </summary>
    /// <param name="timeToWait">in seconds</param>
    /// <returns></returns>
    protected async Task<int> Countdown(float timeToWait)
    {
        var time = 0f;
        while (time < timeToWait)
        {
            time += Time.deltaTime;
            await Task.Yield();
        }
        return 0;
    }
}