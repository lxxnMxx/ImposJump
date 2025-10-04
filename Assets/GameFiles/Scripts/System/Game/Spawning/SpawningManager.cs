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
        TokenSource?.Dispose();
        TokenSource = new CancellationTokenSource();
        _cancelToken = TokenSource.Token;
        await Task.Run(() => Spawn(_cancelToken));
    }

    private void GameOver()
    {
        TokenSource.Cancel();
    }
    
    protected abstract void Spawn(CancellationToken cancelToken);
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
        while (time < timeToWait || cancelToken.IsCancellationRequested)
        {
            Task.Delay(100, cancelToken).Wait(cancelToken);
            time += 0.05f;
            await Task.Yield();
        }

        return 0;
    }

    /// <summary>
    /// Generates a floating-point number between the min and max value.
    /// </summary>
    /// <param name="min">the lowest possible number</param>
    /// <param name="max">the highest possible number</param>
    /// <returns></returns>
    protected async Task<float> RandomNumber(float min, float max)
    {
        print(_random.NextDouble());
        if(min > max) throw new ArgumentOutOfRangeException(nameof(min), min, "Min must be less than or equal to max.");
        return (float)(_random.NextDouble() * (max - min) + min);
    }
}
