using UnityEngine;
using static System.Math;

public static class CombatSystem
{
    public static int CalculateDamage(int attack, int defense)
    {
        return Max((attack - defense), 0);
    }

    public static bool TryFlee(float chance = 0.5f)
    {
        return UnityEngine.Random.value < chance;
    }

    public static int ApplyDefense(int originalDefense, bool isDefending)
    {
        return isDefending ? (int)(originalDefense * 1.5f) : originalDefense;
    }
}
