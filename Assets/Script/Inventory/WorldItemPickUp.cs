using System;
using UnityEditor;
using UnityEngine;

public class WorldItemPickup : MonoBehaviour, IInteractable
{
    public SpriteRenderer spriteRenderer;
    public ItemData ItemData => itemData;
    public int Amount => amount;
    
    [SerializeField] private ItemData itemData;
    [SerializeField] private int amount = 1;

    private void OnValidate()
    {
        if (itemData is null) { return; }
        InitializePickUp(itemData);
    }

    public void SetItemPickUpData(ItemData data)
    {
        itemData = data;
        InitializePickUp(itemData);
    }
    
    private void InitializePickUp(ItemData newData)
    {
        if (newData is null) return;
        spriteRenderer.sprite = newData.itemSprite;
    }
    
    public bool OnInteract(GameObject interactor)
    {
        throw new NotImplementedException();
    }

    public bool OnIsActive()
    {
        throw new NotImplementedException();
    }
}