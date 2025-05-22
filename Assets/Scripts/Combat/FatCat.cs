using UnityEngine;
using UnityEngine.SceneManagement;

public class FatCat : Enemy
{
    public override void Attack(PlayerController player)
    {
        int calculatedDamage = CombatSystem.CalculateDamage(damage, player.defense);
        player.TakeDamage(calculatedDamage);
        Debug.Log("FatCat attacks");
    }
    
    protected override void Defeat()
    {
        base.Defeat();
        Debug.Log("FatCat defeated");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.SetNextEnemy("FatCat");
            SceneManager.LoadScene("CombatScreen");
        }
    }
}
