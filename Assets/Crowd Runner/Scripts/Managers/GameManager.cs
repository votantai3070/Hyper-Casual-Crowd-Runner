using System;
using UnityEngine;

public enum GameState { Menu, Game, LevelComplete, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameState gameState;
    public static Action<GameState> onGameStateChanged;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //ResetLevelGame();
    }

    [ContextMenu("Reset Level")]
    private void ResetLevelGame() => PlayerPrefs.DeleteAll();

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);

        Debug.Log("Game State Changed To " + gameState);
    }

    internal bool IsGameState() => gameState == GameState.Game;

}
