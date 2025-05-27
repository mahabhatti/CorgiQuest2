using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    public Transform teleportTarget;
    public BiomeState SetBiome;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && teleportTarget != null)
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.CancelMove();
            }
            
            other.transform.position = teleportTarget.position;
            GameController.Instance.SetBiomeState(SetBiome);
            PlayerStats.Instance.currentHealth = PlayerStats.Instance.maxHealth;
        }
    }
}