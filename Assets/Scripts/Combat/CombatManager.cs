using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WaitForSeconds = UnityEngine.WaitForSeconds;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public PlayerStats player;
    public Enemy currentEnemy;
    public Text NarrationText;
    private string playerChoice = "";
    private bool actionChosen = false;


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
            player = FindFirstObjectByType<PlayerStats>();
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

    public void StartCombat(string enemyString)
    {
        player = PlayerStats.Instance;
        
        switch (enemyString)
        {
            case "Cat":
                currentEnemy = ScriptableObject.CreateInstance<Cat>();
                break;
            case "SwordCat":
                currentEnemy = ScriptableObject.CreateInstance<Cat>();
                break;
            case "FatCat":
                currentEnemy = ScriptableObject.CreateInstance<Cat>();
                break;
        }
        GameController.Instance.SetGameState(GameState.Combat);
        Debug.Log($"Combat with: {currentEnemy.enemyName}");

        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("Player's turn");

        while (true)
        {
            SetNarration("Choose your action: Attack, Defend, or Heal.");
            playerChoice = "";
            actionChosen = false;

            // Wait for button click
            yield return new WaitUntil(() => actionChosen);

            switch (playerChoice)
            {
                case "attack":
                    int damage = CombatSystem.CalculateDamage(player.currentDamage, currentEnemy.defense);
                    currentEnemy.TakeDamage(damage);
                    SetNarration($"You attack and deal {damage} damage!");
                    break;

                case "defend":
                    player.isDefending = true;
                    SetNarration("You brace for the next attack.");
                    break;

                case "heal":
                    if (player.currentHeals <= 0)
                    {
                        SetNarration("No heals remaining. Choose another action.");
                        actionChosen = false;
                        yield return new WaitUntil(() => actionChosen);
                        continue; // retry
                    }
                    else
                    {
                        player.Heal();
                        SetNarration($"You healed for 10 HP!");
                    }
                    break;

                default:
                    SetNarration("Invalid action.");
                    continue;
            }

            yield return new WaitForSeconds(1f);
            break;  // exit loop if action was valid
        }

            enemyHPBar.value = currentEnemy.currentHealth;
            
            if (currentEnemy.IsDefeated())
            {
                SetNarration($"You defeated {currentEnemy.enemyName}!");
                GameController.Instance.SetGameState(GameState.Win);
                yield break;
            }
        } 
        StartCoroutine(EnemyTurn());
    }


    private IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy's turn");
        SetNarration($"{currentEnemy.enemyName} is attacking!");

        yield return new WaitForSeconds(1f);

        currentEnemy.Attack(player);

        yield return new WaitForSeconds(1f);

        if (player.currentHealth <= 0)
        {
            SetNarration("You were defeated...");
            GameController.Instance.SetGameState(GameState.Loss);
            yield break;
        }

        // Reset defense status
        player.isDefending = false;

        StartCoroutine(PlayerTurn());
    }
    
    public void OnAttackButton()
    {
        playerChoice = "attack";
        actionChosen = true;
    }

    public void OnDefendButton()
    {
        playerChoice = "defend";
        actionChosen = true;
    }

    public void OnHealButton()
    {
        playerChoice = "heal";
        actionChosen = true;
    }

    private void SetNarration(string message)
    {
        if (NarrationText != null)
        {
            NarrationText.text = message;
        }
    }
}