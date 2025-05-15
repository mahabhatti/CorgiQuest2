using UnityEngine;

public enum GameState
{
    TitleScreen,
    Overworld,
    Combat,
    Pause,
    Inventory,
    Win,
    Loss
}

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameState CurrentGameState = GameState.TitleScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;
        Debug.Log("Game state changed to: " + newState);

        switch (newState)
        {
            case GameState.Combat:
                // pause player movement, load combat UI
                break;
            case GameState.Overworld:
                // continue player control
                break;
            case GameState.Pause:
                // show pause screen
                break;
            case GameState.Inventory:
                // show inventory ui overlay
                break;
            case GameState.Win:
                // trigger win screen
                break;
            case GameState.Loss:
                // trigger loss screen or respawn
                break;
            case GameState.TitleScreen:
                // return to main menu
                break;
        }
    }
}