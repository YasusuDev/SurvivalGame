using UnityEngine;

public class Workbench : MonoBehaviour
{
    public Recipe recipe; // Receita que o workbench faz

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Tentando craftar...");

            // Busca o InventoryManager na cena (mesmo estando na UI)
            InventoryManager inventory = FindObjectOfType<InventoryManager>();
            if (inventory == null)
            {
                Debug.LogWarning("Inventário não encontrado na cena!");
                return;
            }

            TryCraft(inventory);
        }
    }

    void TryCraft(InventoryManager inventory)
    {
        bool canCraft = true;

        // Verifica se o jogador possui todos os itens necessários
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            int amountInInventory = inventory.CountItem(recipe.requiredItems[i]);
            Debug.Log($"Verificando item: {recipe.requiredItems[i].name}, quantidade no inventário: {amountInInventory}, necessária: {recipe.requiredAmounts[i]}");

            if (amountInInventory < recipe.requiredAmounts[i])
            {
                canCraft = false;
            }
        }

        if (canCraft)
        {
            Debug.Log("Todos os itens necessários estão presentes. Craftando...");

            // Remove os itens usados
            for (int i = 0; i < recipe.requiredItems.Length; i++)
            {
                inventory.RemoveItem(recipe.requiredItems[i], recipe.requiredAmounts[i]);
                Debug.Log($"Removido {recipe.requiredAmounts[i]}x {recipe.requiredItems[i].name}");
            }

            // Adiciona o item resultante
            bool added = inventory.AddItem(recipe.result);
            if (added)
            {
                Debug.Log($"Crafted {recipe.result.name}!");
            }
            else
            {
                Debug.LogWarning("Inventário cheio! Não foi possível adicionar o item craftado.");
            }
        }
        else
        {
            Debug.Log("Você não tem os itens necessários!");
        }
    }
}
