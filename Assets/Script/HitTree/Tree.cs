using UnityEngine;

public class Tree : MonoBehaviour
{
    public ItemData woodItemData;   // ScriptableObject do item "Wood"
    public Transform dropPoint;     // Onde a madeira vai aparecer
    public int dropAmount = 1;      // Quantidade por hit

    public void Hit(Vector3 playerPosition)
    {
        if (woodItemData is not null && woodItemData.worldPrefab is not null && dropPoint is not null)
        {
            // Instancia o prefab definido no ItemData
            GameObject drop = Instantiate(woodItemData.worldPrefab, dropPoint.position, Quaternion.identity);

            // Se o prefab tiver o script WorldItemPickup, configuramos os dados
            WorldItemPickup pickup = drop.GetComponent<WorldItemPickup>();
            if (pickup is not null)
            {
                pickup.SetItemData(woodItemData, dropAmount);
            }
        }

        Debug.Log($"Dropado {dropAmount}x {woodItemData.itemName}");
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStateManager player = other.GetComponent<PlayerStateManager>();
        if (player is not null && player.playerHit)
        {
            Hit(player.transform.position);
            player.playerHit = false;
        }
    }
}