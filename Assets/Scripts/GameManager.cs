using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState;
    public UnityEvent<GameState> OnGameStateChanged = new UnityEvent<GameState>();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        //In bios
        currentGameState = GameState.Bios;
    }

    public void ChangeState(GameState newState)
    {
        currentGameState = newState;
        OnGameStateChanged?.Invoke(currentGameState);
    }

    public void ChangeToParameter()
    {
        ChangeState(GameState.Parameter);
    }
}

public enum GameState
{
    Bios,
    StartingMenu,
    MainOS,
    Parameter,
    EndOS,
    FinalScene
}
