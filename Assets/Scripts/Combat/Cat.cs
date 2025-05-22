using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : Enemy
{
    public override void Attack(PlayerController player)
    {
        int calculatedDamage = CombatSystem.CalculateDamage(damage + 1, player.defense);
        player.TakeDamage(calculatedDamage);
        Debug.Log("The cat scratches");
    }

    protected override void Defeat()
    {
        base.Defeat();
        Debug.Log("The cat lost and vanishes");
        // drop item, add animation
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.SetNextEnemy("Cat");
            SceneManager.LoadScene("CombatScreen");
        }
    }
}