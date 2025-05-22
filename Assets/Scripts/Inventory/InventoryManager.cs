using System.Collections.Generic;
using UnityEngine;

// A simple singleton that survives scene loads
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    // List of Items AKA inventory
    private  List<Item> items = new List<Item>();

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

   
    public void AddItem(Item item)
    {
        //add items to inventory
        items.Add(item);
    }

    public bool RemoveItem(Item item)
    {
        //remove items from inventory
        return items.Remove(item);
    }

    
    public IReadOnlyList<Item> GetAllItems()
    {
        //only read all items in the inventory
        return items;
    }

 
    public void Clear()
    {
        //clear the inventory list
        items.Clear();
    }
}