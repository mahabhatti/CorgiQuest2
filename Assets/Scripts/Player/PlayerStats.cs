using System;
using UnityEngine;
using static System.Math;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int maxHealth = 20;
    public int currentHealth;
    public int initialDamage = 3;
    public int currentDamage;
    public int initialDefense = 1;
    public int currentDefense;
    public int maxHeals = 1;
    public int currentHeals;
    public int healHealth = 10;
    public bool isDefending = false;
    public Vector2 savedPosition;

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

    public void IncreaseHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = Min(currentHealth + amount, maxHealth);
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
    
    public void TakeDamage(int incomingDamage)
    {
        currentHealth -= incomingDamage;
        currentHealth = Mathf.Max(currentHealth, 0);
        
        Debug.Log($"Sandie took {incomingDamage} damage. Current HP: {currentHealth}");
    }

    public void Heal()
    {
        currentHealth = Mathf.Max(maxHealth, currentHealth + healHealth);
        currentHeals--;
    }
    
    public void SavePosition()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            savedPosition = player.transform.position;
        }
    }

    public void RestorePosition()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = savedPosition;
        }
    }

    public void ResetPosition()
    {
        savedPosition = new Vector2((float)36.50, (float)0.76575);
    }

}
