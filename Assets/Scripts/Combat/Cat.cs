using System;
using UnityEngine;

public class Cat : Enemy
{
    public override void Attack(PlayerStats player)
    {
        int calculatedDamage = CombatSystem.CalculateDamage(damage + 1, player.currentDefense);
        player.TakeDamage(calculatedDamage);
        Debug.Log("The cat scratches");
    }

    protected override void Defeat()
    {
        base.Defeat();
        Debug.Log("The cat lost and vanishes");
        // drop item, add animation
    }

    
}