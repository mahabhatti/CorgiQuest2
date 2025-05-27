using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WaitForSeconds = UnityEngine.WaitForSeconds;
using UnityEngine.SceneManagement;

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
    
    public Slider playerHPBar;
    
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

            print("Creating: " + currentEnemy.enemyName);
            enemyNameText.text = currentEnemy.enemyName;
            enemySpriteImage.sprite = currentEnemy.enemySprite;
            enemyHPBar.maxValue = currentEnemy.maxHealth;
            enemyHPBar.value = currentEnemy.currentHealth;
            playerHPBar.maxValue = player.maxHealth;
            playerHPBar.value = player.currentHealth;
            
            StartCombat(currentEnemy);
        }
        else
        {
            Debug.LogError($"enemy prefab '{enemyPrefabName}' not found");
        }
    }

    public void StartCombat(Enemy curr)
    {
        player = PlayerStats.Instance;
        player.currentHeals = player.maxHeals;
        
        GameController.Instance.SetGameState(GameState.Combat);
        Debug.Log($"Combat with: {curr.enemyName}");

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
                        SetNarration($"You healed for {player.healHealth} HP!");
                    }
                    break;

                default:
                    SetNarration("Invalid action.");
                    continue;
            }
            
            enemyHPBar.value = currentEnemy.currentHealth;
            
            yield return new WaitForSeconds(2f);
            break;  // exit loop if action was valid
        }
        
        if (currentEnemy.IsDefeated())
        {
            SetNarration($"You defeated {currentEnemy.enemyName}! +3 max health!");
            PlayerStats.Instance.IncreaseHealth(3);
            yield return new WaitForSeconds(3f);
            GameController.Instance.MarkEnemyDefeated();
            GameController.Instance.EndCombat();
        }
        
        StartCoroutine(EnemyTurn());
    }


    private IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy's turn");

        if (currentEnemy.isCharging)
        {
            SetNarration($"{currentEnemy.enemyName} unleashes a powerful attack!");
            yield return new WaitForSeconds(1f);

            SetNarration(currentEnemy.Attack(player, 2)); // Use 2x damage
            currentEnemy.isCharging = false;
        }
        else
        {
            // 25% chance to charge
            if (Random.value < 0.25f)
            {
                currentEnemy.isCharging = true;
                SetNarration($"{currentEnemy.enemyName} is charging up a powerful attack!");
            }
            else
            {
                SetNarration($"{currentEnemy.enemyName} is attacking!");
                yield return new WaitForSeconds(1f);

                SetNarration(currentEnemy.Attack(player, 1)); // Normal attack
            }
        }

        playerHPBar.value = player.currentHealth;
        yield return new WaitForSeconds(2f);

        if (player.currentHealth <= 0)
        {
            SetNarration("You were defeated...");
            yield return new WaitForSeconds(5f);
            GameController.Instance.GameOver();
            yield break;
        }

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