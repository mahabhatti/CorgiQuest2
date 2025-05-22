using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;       // the inventory panel
    public Transform contentParent;         // the content layout
    public GameObject itemSlotPrefab;       // item

    void Start()
    {
        //panel should not show at start of game
        inventoryPanel.SetActive(false);
    }

    
    public void ToggleInventory()
    {   
        // using a button
        bool show = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(show);
        if (show) RefreshInventory();
    }

    void RefreshInventory()
    {
        // Clear all the old entries
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Populate new ones
        foreach (Item item in InventoryManager.Instance.GetAllItems())
        {
            GameObject slot = Instantiate(itemSlotPrefab, contentParent);
            Text label = slot.GetComponentInChildren<Text>();
            label.text = item.itemName; 
        }
    }
}