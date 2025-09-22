using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    
    private void Start()
    {
        //seleciona primeiro slot no inicio do jogo
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 6)
            {
                ChangeSelectedSlot(number-1);
            }
        }
    }
    void ChangeSelectedSlot(int newValue)
    {

        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    public bool AddItem(Item item)
    {
        //check if any slot has the same item with count lower than max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot invSlot = inventorySlots[i];
            InventoryItem itemInSlot = invSlot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        
        //find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot invSlot = inventorySlots[i];
            InventoryItem itemInSlot = invSlot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, invSlot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot invSlot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = invSlot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
           Item item = itemInSlot.item;
           if (use == true) // decrementa
           {
               itemInSlot.count--;
               if (itemInSlot.count <= 0)
               {
                   Destroy(itemInSlot.gameObject);
               }
               else
               {
                   itemInSlot.RefreshCount();
               }
           }
           return item;
        }
        return null;
    }
    //QUALQUER COISA APAGAR DAQUI PRA BAIXO
    public int CountItem(Item item)
    {
        int total = 0;
        foreach (var slot in inventorySlots)
        {
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem != null && inventoryItem.item == item)
            {
                total += inventoryItem.count;
            }
        }
        return total;
    }

    public void RemoveItem(Item item, int amount)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItem inventoryItem = inventorySlots[i].GetComponentInChildren<InventoryItem>();
            if (inventoryItem != null && inventoryItem.item == item)
            {
                int removeCount = Mathf.Min(amount, inventoryItem.count);
                inventoryItem.count -= removeCount;
                amount -= removeCount;
                inventoryItem.RefreshCount();
            
                if (inventoryItem.count <= 0)
                {
                    Destroy(inventoryItem.gameObject);
                }

                if (amount <= 0) break;
            }
        }
    }

}
