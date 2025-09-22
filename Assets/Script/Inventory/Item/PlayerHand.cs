using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Transform handPoint; // arraste aqui o empty na mão
    private GameObject currentItemInHand;
    private Item currentEquippedItem;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager não encontrado na cena!");
        }
    }

    private void Update()
    {
        if (inventoryManager == null) return;

        // pega o item selecionado sem consumi-lo
        Item selected = inventoryManager.GetSelectedItem(false);
        UpdateHand(selected);
    }

    void UpdateHand(Item item)
    {
        // se o item atual na mão é diferente do selecionado, trocar
        if (currentEquippedItem != item)
        {
            // destruir o atual se existir
            if (currentItemInHand != null)
            {
                Destroy(currentItemInHand);
                currentItemInHand = null;
                currentEquippedItem = null;
            }

            // instanciar novo se houver handPrefab
            if (item != null && item.handPrefab != null)
            {
                currentItemInHand = Instantiate(item.handPrefab, handPoint);
                currentItemInHand.transform.localPosition = Vector3.zero;
                currentItemInHand.transform.localRotation = Quaternion.identity;
                currentItemInHand.transform.localScale = Vector3.one; // ajuste se necessário
                currentEquippedItem = item;
                Debug.Log($"Equipado na mão: {item.name}");
            }
            else if (item != null)
            {
                Debug.Log($"Item {item.name} selecionado mas não tem handPrefab definido.");
            }
        }
    }
}