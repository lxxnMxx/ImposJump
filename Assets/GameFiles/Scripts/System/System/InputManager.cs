using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : Singleton<InputManager>
{
    private GameState _gameState;
    
    
    public void ReturnActionPerformed(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        _gameState = GameManager.Instance.GameState;
        
        if (GameManager.Instance.IsPlaying)
        {
            if (!SceneHandler.Instance.IsCurrentSceneTutorial())
                LevelManager.Instance.GetActiveLevel().deathCount += 1;
            ButtonManager.Instance.Reset();
        }
        else switch (_gameState)
        {
            case GameState.GameOver:
                ButtonManager.Instance.Reset();
                break;
            case GameState.Pause:
                ButtonManager.Instance.Resume();
                break;
            default:
                Debug.LogError("GameState is not supported (InputManager l. 31)");
                return;
        }
    }

    public void OnPauseMenuPerformed(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (GameManager.Instance.IsPlaying)
        {
            GameManager.Instance.ChangeGameState(GameState.Pause);
        }
        else if (GameManager.Instance.GameState ==  GameState.Pause)
            ButtonManager.Instance.Resume();
    }
}
