using UnityEngine;


public class ItemCollector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Only pick up objects with the "Collectible" tag
        if (!other.CompareTag("Collectible"))
        {
            return;
        }

        // Get the Item 
        var item = other.GetComponent<Item>();
        //Add it to the inventory
        if (item != null)
        {
            InventoryManager.Instance.AddItem(item);
            Destroy(other.gameObject);
        }
    }
}