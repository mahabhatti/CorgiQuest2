// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// public class EnemyEncounter : MonoBehaviour
// {
//     public string enemyID;
//
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             GameController.Instance.SetCurrentEnemy(enemyID);
// 			CombatManager combatManager = FindFirstObjectByType<CombatManager>();
//             combatManager.StartCombat(enemyID);
//             GetComponent<Collider2D>().enabled = false;
//             SceneManager.LoadScene("CombatScene");
// 			
//         }
//     }
// }
//
