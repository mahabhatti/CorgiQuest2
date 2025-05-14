using UnityEngine;

/// <summary>
/// Attach this to your Player GameObject.
/// When the Player touches a Trigger‐collider tagged "Collectible",
/// it grabs the Item component and adds it to the InventoryManager.
/// </summary>
public class ItemCollector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Only pick up objects tagged "Collectible"
        if (!other.CompareTag("Collectible")) 
            return;

        // Get the Item component
        var item = other.GetComponent<Item>();
        if (item != null)
        {
            InventoryManager.Instance.AddItem(item);
            Destroy(other.gameObject);
        }
    }
}