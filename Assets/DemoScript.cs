using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    
    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("Item Added : DemoScript.cs");
        }
        else
        {
            Debug.Log("Item NOT Added : DemoScript.cs");
        }
            
    }

    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Get Selected Item DemoScript.cs: " + receivedItem);
        }
        else
        {
            Debug.Log("NOT Get Selected Item DemoScript.cs");
            
        }
    }
    
    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used Item DemoScript.cs: " + receivedItem);
        }
        else
        {
            Debug.Log("Item NOT Used DemoScript.cs");
            
        }
    }
}
