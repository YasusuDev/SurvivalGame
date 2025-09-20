using System;
using UnityEngine;

public class WorldItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int amount = 1;

    public SpriteRenderer spriteRenderer;
    public ItemData ItemData => itemData;
    public int Amount => amount;

    private bool _isActive = true;

    private void OnValidate()
    {
        if (itemData is not null)
        {
            SetItemData(itemData);
        }
    }
    
    public void Awake()
    {
        if (itemData is not null)
        {
            SetItemData(itemData);
        }
    }

    public void SetItemData(ItemData newData, int newAmount = 1)
    {
        if (newData is null)
        {
            Debug.LogError("ItemData é NULL!");
            return;
        }

        itemData = newData;
        amount = newAmount;

        if (spriteRenderer is not null)
        {
            spriteRenderer.sprite = itemData.icon;
        }
    }

    public bool Interact(GameObject interactor)
    {
        Inventory interactorInventory = interactor.GetComponent<Inventory>();
        
        if (interactorInventory is null) { return false; }

        var pickup = gameObject;

        interactorInventory.AddItem(itemData, amount);
        Destroy(gameObject);
        
        return true;
    }

    public bool IsActive()
    {
        return _isActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<CharacterController>();
        _isActive = player;
    }
}