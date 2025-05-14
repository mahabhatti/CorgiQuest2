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

    void Awake()
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
    }
}