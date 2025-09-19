using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager : MonoBehaviour
{
    public GameObject tree;
    
    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Input = GetComponent<PlayerInput>();
        PlayerSpeed = 10f;
        PlayerRotateSpeed = 180f;
        CameraAngle = 0f;
        cameraOffset = new Vector3(0f, 10f, -10f);
        cameraFollowSpeed = 10f;
        _gravityVector = new Vector3(0f, -9.81f, 0f);
    }

    void Update()
    {
        HandleMovement();
        HandleCamera();
        HandleAttack();
    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }

    #region Movement

    private void HandleMovement()
    {
        Vector3 move = transform.rotation * MoveVector;
        Controller.Move(PlayerSpeed * move * Time.deltaTime);
        Controller.Move(_gravityVector * Time.deltaTime);
    }

    #endregion

    #region Camera

    private void HandleCamera()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, CameraAngle, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, PlayerRotateSpeed * Time.deltaTime);
    }

    private void UpdateCameraPosition()
    {
        if (playerPosition == null) return;
        Vector3 desiredPosition = transform.position + transform.rotation * cameraOffset;
        playerPosition.position = Vector3.Lerp(playerPosition.position, desiredPosition, cameraFollowSpeed * Time.deltaTime);
        playerPosition.LookAt(transform.position + Vector3.up * 2f);
    }

    #endregion

    #region Attack

    private void HandleAttack()
    {
        if (!playerHit) return;

        // Primeiro verifica árvore
        Vector3 attackPoint = transform.position + transform.forward * 1.5f;
        Collider[] hits = Physics.OverlapBox(attackPoint, new Vector3(0.5f, 1f, 0.5f));
        bool hitSomething = false;

        foreach (Collider col in hits)
        {
            Tree tree = col.GetComponent<Tree>();
            if(tree != null)
            {
                tree.Hit(transform.position);
                hitSomething = true;
                break; // só ataca 1 árvore
            }
        }

        // Se não acertou árvore, verifica dummy
        if(!hitSomething)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position + Vector3.up * 1f, transform.forward, out hit, 2f))
            {
                TrainingDummy dummy = hit.collider.GetComponent<TrainingDummy>();
                if(dummy != null)
                    dummy.TakeHit();
            }
        }

        playerHit = false; // reseta o hit
    }

    #endregion
}
