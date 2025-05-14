using UnityEngine;
using static System.Math;

public class CombatSystem : MonoBehaviour
{
    public enum CombatState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose,
        Flee,
        Busy
    }
    
    public static int CalculateDamage(int attack, int defense)
    {
        return Max((attack - defense), 0);
    }

    public static int RunPlayerTurn()
    {
        return 1;
    }
}
