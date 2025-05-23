using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTrigger : MonoBehaviour
{
    public string enemyPrefabName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.SetCurrentEnemy(enemyPrefabName);
            SceneManager.LoadScene("CombatScreen");
        }
    }
}
