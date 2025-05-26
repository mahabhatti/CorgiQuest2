using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordCat : Enemy
{
    public override void Attack(PlayerStats player)
    {
        int calculatedDamage = CombatSystem.CalculateDamage(damage + 3, player.currentDefense);
        player.TakeDamage(calculatedDamage);
        Debug.Log("SwordCat slashes");
    }

    protected override void Defeat()
    {
        base.Defeat();
        Debug.Log("SwordCat defeated");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.SetCurrentEnemy("SwordCat");
            SceneManager.LoadScene("CombatScreen");
        }
    }
}
