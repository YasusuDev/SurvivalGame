using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerStateManager
{
    public CharacterController Controller;
    public Transform playerPosition;
    public PlayerInput Input;

    public Vector3 MoveVector;
    public Vector2 InputVector;

    public float PlayerSpeed;
    public float PlayerRotateSpeed;

    public bool playerHit;

    public Vector3 cameraOffset;
    public float cameraFollowSpeed;
    public float CameraAngle;
    public Vector3 _gravityVector;
    
    public GameObject overlappedObject;
}