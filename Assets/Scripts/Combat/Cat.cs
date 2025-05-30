using UnityEngine;

public class Cat : Enemy
{
    public override string Attack(PlayerStats player, int multiplier)
    {
        int multipliedDamage = damage * multiplier;
        int effectiveDefense = CombatSystem.ApplyDefense(player.currentDefense, player.isDefending);
        int calculatedDamage = CombatSystem.CalculateDamage(multipliedDamage, effectiveDefense);
        player.TakeDamage(calculatedDamage);
        string narration = $"The cat scratches with its claws for {calculatedDamage} damage!";
        Debug.Log(narration);
        return narration;
    }
}