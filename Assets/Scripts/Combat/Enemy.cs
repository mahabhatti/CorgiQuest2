using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Enemy";
    public int maxHealth = 30;
    public int currentHealth;
    public int damage = 5;

    public Sprite enemySprite;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
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

    public virtual void Attack(PlayerController player)
    {
        int calculatedDamage = CombatSystem.CalculateDamage(damage, player.defense);
        player.TakeDamage(calculatedDamage);
        Debug.Log($"{enemyName} attacks Sandie for {damage} damage.");
    }

    public bool IsDefeated()
    {
        return currentHealth <= 0;
    }

    protected virtual void Defeat()
    {
        Debug.Log($"{enemyName} has been defeated.");
        gameObject.SetActive(false);
    }
}