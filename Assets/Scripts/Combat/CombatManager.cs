using UnityEngine;
using System.Collections;
using WaitForSeconds = UnityEngine.WaitForSeconds;

public class CombatManager : MonoBehaviour
{
    public PlayerController player;
    public Enemy currentEnemy;

    private void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<PlayerController>();
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
            
        } 
        
        int damage = CombatSystem.CalculateDamage(10, currentEnemy.defense); // placeholder values
        currentEnemy.TakeDamage(damage);

        if (currentEnemy.IsDefeated())
        {
            GameController.Instance.SetGameState(GameState.Win);
            yield break;
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