using System.Collections.Generic;
using UnityEngine;

// A simple singleton that survives scene loads
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    // Change Item to whatever class/struct you use
    private  List<Item> _items = new List<Item>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Add a new item to the global inventory.
    /// </summary>
    public void AddItem(Item item)
    {
        _items.Add(item);
        Debug.Log($"Added {item.itemName}. Total items: {_items.Count}");
    }

    /// <summary>
    /// Remove item if present; returns true on success.
    /// </summary>
    public bool RemoveItem(Item item)
    {
        return _items.Remove(item);
    }

    /// <summary>
    /// Read-only view for UI or logic.
    /// </summary>
    public IReadOnlyList<Item> GetAllItems()
    {
        return _items;
    }

    /// <summary>
    /// Clear inventory (e.g. on game over).
    /// </summary>
    public void Clear()
    {
        _items.Clear();
    }
}