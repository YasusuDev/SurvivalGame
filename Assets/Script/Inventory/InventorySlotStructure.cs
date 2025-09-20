using System;
using UnityEngine;

[Serializable]
public class InventorySlotStructure
{
    public string guid;
    public ItemData itemData;
    public int quantity;
    public int maxStackSize;

    public InventorySlotStructure(ItemData item, int amount)
    {
        guid = Guid.NewGuid().ToString();
        itemData = item;
        quantity = amount;
        maxStackSize = item.maxStackSize;
    }

    public bool IsFull => quantity >= maxStackSize;

    public int AddQuantity(int amount)
    {
        int spaceLeft = maxStackSize - quantity;
        int added = Mathf.Min(amount, spaceLeft);
        quantity += added;
        return added;
    }

    public int RemoveQuantity(int amount)
    {
        int removed = Mathf.Min(amount, quantity);
        quantity -= removed;
        return removed;
    }
}