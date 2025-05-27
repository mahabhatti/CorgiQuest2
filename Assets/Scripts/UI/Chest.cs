using System;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;

    private SpriteRenderer _spriteRenderer;
    private bool isOpened = false;

    public int addDamage = 0;
    public int addDefense = 0;
    public int addHeals = 0;
    public string chestContents;
    
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

        PlayerStats.Instance.IncreaseDamage(addDamage);
        PlayerStats.Instance.IncreaseDefense(addDefense);
        PlayerStats.Instance.IncreaseHeals(addHeals);
        
        UIManager.Instance.ShowPopup($"Added {chestContents}!",displayDuration);

    }
}