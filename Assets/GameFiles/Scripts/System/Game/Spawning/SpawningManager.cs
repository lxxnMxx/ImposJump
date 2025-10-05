using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public abstract class SpawningManager : Singleton<SpawningManager>
{
    protected CancellationTokenSource TokenSource;
    private CancellationToken _cancelToken;

    private System.Random _random;

    new void Awake()
    {
        _random = new System.Random();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += LevelStart;
        GameManager.Instance.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= LevelStart;
        GameManager.Instance.OnGameOver -= GameOver;
    }

    private async void LevelStart()
    {
        if(_cancelToken.CanBeCanceled) TokenSource.Cancel();
        TokenSource?.Dispose();
        
        TokenSource = new CancellationTokenSource();
        _cancelToken = TokenSource.Token;
        await Spawn(_cancelToken);
    }

    private void GameOver()
    {
        TokenSource.Cancel();
    }
    
    protected abstract Task<int> Spawn(CancellationToken cancelToken);
    protected abstract Task<int> Despawn(CancellationToken cancelToken, GameObject go, int poolingIndex);
    protected abstract Task<int> Despawn(CancellationToken cancelToken, GameObject go);

    /// <summary>
    /// Just a countdown, that counts down the time.
    /// </summary>
    /// <param name="cancelToken">canllationToken</param>
    /// <param name="timeToWait">in seconds</param>
    /// <returns></returns>
    protected async Task<int> Countdown(CancellationToken cancelToken, float timeToWait)
    {
        var time = 0f;
        while (time < timeToWait)
        {
            if(cancelToken.IsCancellationRequested)
                throw new TaskCanceledException();
            
            if(Time.timeScale > 0)
                time += Time.deltaTime;
            await Task.Yield();
        }

        return 0;
    }
}
