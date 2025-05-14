using UnityEngine;
using static System.Math;

public static class CombatSystem
{
    public static int CalculateDamage(int attack, int defense)
    {
        return Max((attack - defense), 0);
    }
}
