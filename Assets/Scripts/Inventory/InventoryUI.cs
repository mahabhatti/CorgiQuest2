using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;       // drag in your InventoryPanel
    public Transform contentParent;         // the ScrollView content or Layout object
    public GameObject itemSlotPrefab;       // your ItemSlot prefab

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    // Wire this to the Button's OnClick
    public void ToggleInventory()
    {
        bool show = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(show);
        if (show) RefreshInventory();
    }

    void RefreshInventory()
    {
        // Clear old entries
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Populate new ones
        foreach (Item item in InventoryManager.Instance.GetAllItems())
        {
            GameObject slot = Instantiate(itemSlotPrefab, contentParent);
            Text label = slot.GetComponentInChildren<Text>();
            label.text = item.itemName;
            // you could also set an Image component for item icons here
        }
    }
}