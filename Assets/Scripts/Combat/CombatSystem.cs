using UnityEngine;
using static System.Math;

public static class CombatSystem
{
    public static int CalculateDamage(int attack, int defense)
    {
        return Max((attack - defense), 1);
    }

    public static int ApplyDefense(int originalDefense, bool isDefending)
    {
        return isDefending ? (int)(originalDefense * 3f) : originalDefense;
    }
}
