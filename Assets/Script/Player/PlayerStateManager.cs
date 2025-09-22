using System.Linq;
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
        if (playerPosition is null) return;
        
        Vector3 desiredPosition = transform.position + transform.rotation * cameraOffset;
        playerPosition.position = Vector3.Lerp(playerPosition.position, desiredPosition, cameraFollowSpeed * Time.deltaTime);
        playerPosition.LookAt(transform.position + Vector3.up * 2f);
    }

    #endregion

    #region Attack

    private void HandleAttack()
    {
        if (!playerHit) return;

        Vector3 attackPoint = transform.position + transform.forward * 1.5f;
        Vector3 halfExtents = new Vector3(0.5f, 1f, 0.5f);

        Collider[] hits = Physics.OverlapBox(attackPoint, halfExtents, transform.rotation);

        foreach (Collider col in hits)
        {
            TrainingDummy dummy = col.GetComponent<TrainingDummy>();
            if (dummy is not null)
            {
                dummy.TakeHit();
                Debug.Log("Acertou dummy!");
                playerHit = false;
                return;
            }
        }

        playerHit = false;
    }

    #endregion
    
    #region Interaction

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            overlappedObject = other.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (overlappedObject == other.gameObject)
        {
            overlappedObject = null;
        }
    }
    
    void TryInteract()
    {
        if (overlappedObject is null) { return; }
        
        IInteractable interactable = overlappedObject.GetComponent<IInteractable>();
        
        if (interactable == null) return;
        
        bool isActive = interactable.OnIsActive();
        
        if (isActive)
        {
            interactable.OnInteract(gameObject);
        }
    }
    #endregion
}
