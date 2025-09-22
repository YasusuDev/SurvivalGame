using UnityEngine;

public class Loot : MonoBehaviour
{
    public Item item;
    public int amount = 1;

    public void PickUp()
    {
        InventoryManager inventory = FindObjectOfType<InventoryManager>();
        if (inventory != null)
        {
            for (int i = 0; i < amount; i++)
            {
                bool added = inventory.AddItem(item);
                if (!added)
                {
                    Debug.Log("Inventário cheio!");
                    break;
                }
            }
            Destroy(gameObject); // remove do chão
        }
    }

    // Detecta proximidade do jogador
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp();
        }
    }
}