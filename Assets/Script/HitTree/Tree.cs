using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject woodPrefab; // prefab do galho
    public Transform dropPoint;   // onde a madeira vai aparecer

    // Este é o método que o player vai chamar ao atacar
    public void Hit(Vector3 playerPosition)
    {
        // Spawn da madeira
        if (woodPrefab != null && dropPoint != null)
        {
            Instantiate(woodPrefab, dropPoint.position, Quaternion.identity);
        }

        Debug.Log("Madeira gerada!");
    }

    // Opcional: se quiser manter trigger
    private void OnTriggerEnter(Collider other)
    {
        PlayerStateManager player = other.GetComponent<PlayerStateManager>();
        if (player != null && player.playerHit)
        {
            Hit(player.transform.position);
            player.playerHit = false;
        }
    }
}

