using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    public Transform teleportTarget;

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
        }
    }
}