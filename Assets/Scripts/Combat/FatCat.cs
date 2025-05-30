using UnityEngine;

public class FatCat : Enemy
{
    public override string Attack(PlayerStats player, int multiplier)
    {
        int multipliedDamage = damage * multiplier;
        int effectiveDefense = CombatSystem.ApplyDefense(player.currentDefense, player.isDefending);
        int calculatedDamage = CombatSystem.CalculateDamage(multipliedDamage, effectiveDefense);
        player.TakeDamage(calculatedDamage);
        string narration = $"FatCat body slams you for {calculatedDamage} damage!";
        Debug.Log(narration);
        return narration;
    }
}
