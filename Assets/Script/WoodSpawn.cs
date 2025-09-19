using UnityEngine;

public class WoodItem : MonoBehaviour
{
    [Header("Força inicial do quique")]
    public float minForce = 2f;
    public float maxForce = 5f;
    public float upwardForce = 3f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Garantir que a física esteja habilitada
        rb.useGravity = true;
        rb.isKinematic = false;

        ApplySpawnForce();
    }

    void ApplySpawnForce()
    {
        // Vetor aleatório horizontal
        Vector3 horizontal = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized;

        // Multiplica pela força aleatória e adiciona força para cima
        Vector3 force = horizontal * Random.Range(minForce, maxForce) + Vector3.up * upwardForce;

        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse); // Rotação aleatória
    }
}


