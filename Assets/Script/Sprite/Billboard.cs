using UnityEngine;

public class Billboard : MonoBehaviour
{
    [Header("Snap Settings")]
    [SerializeField] private int directions = 8; // número de direções do sprite (8 = 45°)
    
    [Header("Rotation Speed")]
    [SerializeField] private float rotateSpeed = 360f; // graus por segundo

    [Header("Lock Rotation")]
    [SerializeField] private bool lockX = false;
    [SerializeField] private bool lockY = false;
    [SerializeField] private bool lockZ = false;

    private Vector3 originalRotation;
    private Quaternion targetRotation;

    private void Awake()
    {
        originalRotation = transform.rotation.eulerAngles;
        targetRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (Camera.main == null) return;

        // pega a rotação Y da câmera
        float camY = Camera.main.transform.eulerAngles.y;

        // calcula ângulo “snap” baseado nas direções
        float stepAngle = 360f / directions;
        float snappedY = Mathf.Round(camY / stepAngle) * stepAngle;

        // monta a rotação alvo
        Vector3 newRotation = new Vector3(
            lockX ? originalRotation.x : 0f,
            lockY ? originalRotation.y : snappedY,
            lockZ ? originalRotation.z : 0f
        );

        targetRotation = Quaternion.Euler(newRotation);

        // aplica rotação suave
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }
}