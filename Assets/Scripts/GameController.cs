using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameState
{
    TitleScreen,
    Overworld,
    Combat,
    Win,
    Loss
}

public enum BiomeState
{
    Forest,
    Savanna,
    Mountain
}

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameState CurrentGameState = GameState.TitleScreen;
    public BiomeState CurrentBiomeState = BiomeState.Forest;
    
    private string CurrentEnemyID;
    private List<string> defeatedEnemies;
    private Dictionary<BiomeState, Transform> biomeSpawns = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // persist between scenes
            
            defeatedEnemies = new List<string>();
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
    
    public void SetBiomeState(BiomeState newState)
    {
        CurrentBiomeState = newState;
        Debug.Log("Biome state changed to: " + newState);
    }
    
    public void SetCurrentEnemy(string enemyID)
    {
        CurrentEnemyID = enemyID;
        Debug.Log("Current enemy changed to: " + enemyID);
    }

    public void ResetBiomeEnemies()
    {
        defeatedEnemies.Clear();
    }

    public void RegisterSpawnPoint(BiomeState biome, Transform spawn)
    {
        if (!biomeSpawns.ContainsKey(biome))
        {
            biomeSpawns[biome] = spawn;
        }
    }

    public Vector3 GetSpawnPosition()
    {
        if (biomeSpawns.TryGetValue(CurrentBiomeState, out Transform spawn))
            return spawn.position;

        Debug.LogWarning("No spawn point found for biome " + CurrentBiomeState);
        return Vector3.zero;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Overworld");
        LossManager.Instance.ShowLoss();
    }

    public void ResetWorld()
    {
        PlayerStats.Instance.ResetHealth();
        ResetBiomeEnemies();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.CancelMove();
                player.transform.position = GetSpawnPosition();
            }
            else
            {
                Debug.LogWarning("Player Object not found");
            }
        }
        else
        {
            Debug.LogWarning("Player Object not found");
        }
    }
}