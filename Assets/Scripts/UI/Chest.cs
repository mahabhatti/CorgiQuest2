using System;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;

    private SpriteRenderer _spriteRenderer;
    private bool isOpened = false;
    
    public Item itemToAdd;
    
    public float displayDuration = 2f;   


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = closedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpened && other.CompareTag("Player"))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;
        _spriteRenderer.sprite = openSprite;
        // add item to inventory + dialogue of item found
        
        // ─── DIAGNOSTIC LOGS ───────────────────────────────────────────
        Debug.Log($"[{name}] InventoryManager.Instance = {InventoryManager.Instance}");
        Debug.Log($"[{name}] itemToAdd = {itemToAdd}");
        Debug.Log($"[{name}] UIManager.Instance = {UIManager.Instance}");
        // ────────────────────────────────────────────────────────────────

        InventoryManager.Instance.AddItem(itemToAdd);    // ← ADDED
        
        UIManager.Instance.ShowPopup($"Added “{itemToAdd.itemName}”!",displayDuration);

    }
}