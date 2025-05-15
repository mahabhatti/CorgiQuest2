using System;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;

    private SpriteRenderer _spriteRenderer;
    private bool isOpened = false;

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
    }
}