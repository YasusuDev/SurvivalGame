using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> slots = new();
    [SerializeField] private ItemData currentEquippedItem;
    
    public void AddItem(ItemData itemData, int amount = 1)
    {
        if (itemData == null) return;

        var slot = slots.Find(s => s.itemData == itemData && !s.IsFull);
        if (slot != null)
        {
            int added = slot.AddQuantity(amount);
            int leftover = amount - added;
            if (leftover > 0)
                slots.Add(new InventorySlot(itemData, leftover));
        }
        else
        {
            slots.Add(new InventorySlot(itemData, amount));
        }
    }
    
    public int GetTotalAmount(ItemData itemData)
    {
        int total = 0;
        foreach (var slot in slots)
        {
            if (slot.itemData == itemData)
                total += slot.quantity;
        }
        return total;
    }
    
    public bool HasEnough(ItemData itemData, int requiredAmount)
    {
        return GetTotalAmount(itemData) >= requiredAmount;
    }
    
    public void RemoveItems(ItemData itemData, int amount)
    {
        int remaining = amount;
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            var slot = slots[i];
            if (slot.itemData == itemData)
            {
                int removed = slot.RemoveQuantity(remaining);
                remaining -= removed;
                if (slot.quantity <= 0)
                    slots.RemoveAt(i);

                if (remaining <= 0)
                    break;
            }
        }

        if (remaining > 0)
        {
            Debug.LogWarning("Não havia itens suficientes para remover!");
        }
    }

    public ItemData CurrentEquippedItem => currentEquippedItem;
}