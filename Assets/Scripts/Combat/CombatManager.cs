using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WaitForSeconds = UnityEngine.WaitForSeconds;

public class CombatManager : MonoBehaviour
{
    public PlayerController player;
    public Enemy currentEnemy;

    public string enemyPrefabName;
    public Transform enemySpawnPoint;
    private GameObject enemyInstance;

    public Text enemyNameText;
    public Image enemySpriteImage;
    public Slider enemyHPBar;

    private void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<PlayerController>();
        }

        if (string.IsNullOrEmpty(enemyPrefabName))
        {
            enemyPrefabName = GameController.Instance.nextEnemyPrefabName;
        }
        
        if (!string.IsNullOrEmpty(enemyPrefabName))
        {
            LoadEnemy();
        }
        else
        {
            Debug.LogError("set enemy prefab name");
        }
    }

    private void LoadEnemy()
    {
        GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/" + enemyPrefabName);
        if (enemyPrefab != null)
        {
            enemyInstance = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
            currentEnemy = enemyInstance.GetComponent<Enemy>();

            enemyNameText.text = currentEnemy.enemyName;
            enemySpriteImage.sprite = currentEnemy.enemySprite;
            enemyHPBar.maxValue = currentEnemy.maxHealth;
            enemyHPBar.value = currentEnemy.currentHealth;
            
            StartCombat(currentEnemy);
        }
        else
        {
            Debug.LogError($"enemy prefab '{enemyPrefabName}' not found");
        }
    }

    public void StartCombat(Enemy enemy)
    {
        currentEnemy = enemy;
        GameController.Instance.SetGameState(GameState.Combat);
        Debug.Log($"Combat with: {enemy.enemyName}");

        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("Player's turn");
        yield return new WaitForSeconds(1f);

        string playerChoice = "attack";

        if (playerChoice == "attack")
        {
            int damage = CombatSystem.CalculateDamage(10, currentEnemy.defense); // placeholder values
            currentEnemy.TakeDamage(damage);

            enemyHPBar.value = currentEnemy.currentHealth;
            
            if (currentEnemy.IsDefeated())
            {
                GameController.Instance.SetGameState(GameState.Win);
                yield break;
            }
        } 
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy's turn");
        yield return new WaitForSeconds(1f);
        
        currentEnemy.Attack(player);

        if (player.currentHealth <= 0)
        {
            GameController.Instance.SetGameState(GameState.Loss);
            yield break;
        }

        StartCoroutine(PlayerTurn());
    }
}