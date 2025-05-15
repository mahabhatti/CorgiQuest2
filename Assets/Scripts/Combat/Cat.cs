using System;
using UnityEngine;

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
            CombatManager combatManager = FindFirstObjectByType<CombatManager>();
            combatManager.StartCombat(this);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}