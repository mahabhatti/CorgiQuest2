using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int maxHealth = 20;
    public int currentHealth;
    public int initialDamage = 3;
    public int currentDamage;
    public int initialDefense = 3;
    public int currentDefense;
    public int maxHeals = 1;
    public int currentHeals;
    public bool isDefending = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        
        currentHealth = maxHealth;
        currentDamage = initialDamage;
        currentDefense = initialDefense;
        currentHeals = maxHeals;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void IncreaseDamage(int amount)
    {
        currentDamage += amount;
    }

    public void IncreaseDefense(int amount)
    {
        currentDefense += amount;
    }
    
    public void IncreaseHeals(int amount)
    {
        maxHeals += amount;
    }
    
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log($"Sandie took {amount} damage. Current HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Sandie has been defeated.");
            // add trigger loss screen, then respawn at campfire
            GameController.Instance.GameOver();
        }
    }

    public void Heal()
    {
        currentHealth = Mathf.Max(maxHealth, currentHealth + 10);
        currentHeals--;
    }
    
}
