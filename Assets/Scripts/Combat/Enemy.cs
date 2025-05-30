using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Enemy";
    public string enemyID;
    public int maxHealth;
    public int currentHealth;
    public int damage;
    public int defense;
    public bool isCharging = false;

    public Sprite enemySprite;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        
        UpdateVisibility();
    }
    
    private void OnEnable()
    {
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        gameObject.SetActive(!GameController.Instance.IsEnemyDefeated(enemyID));
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        
        Debug.Log($"{enemyName} took {amount} damage. Current HP: {currentHealth}");

        if (IsDefeated())
        {
            Defeat();
        }
    }
    
    public virtual string Attack(PlayerStats player, int multiplier)
    {
        int multipliedDamage = damage * multiplier;
        int effectiveDefense = CombatSystem.ApplyDefense(player.currentDefense, player.isDefending);
        int calculatedDamage = CombatSystem.CalculateDamage(multipliedDamage, effectiveDefense);
        player.TakeDamage(calculatedDamage);
        string narration = $"Enemy attacks for {calculatedDamage} damage!";
        Debug.Log(narration);
        return narration;
    }

    public bool IsDefeated()
    {
        return currentHealth <= 0;
    }

    protected virtual string Defeat()
    {
        string narration = $"{enemyName} has been defeated.";
        Debug.Log(narration);
        return narration;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.SetCurrentEnemy(enemyName, enemyID);
            PlayerStats.Instance.SavePosition();
            SceneManager.LoadScene("CombatScreen");
        }
    }
}