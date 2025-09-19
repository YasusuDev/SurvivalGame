using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public partial class PlayerStateManager
{
    private void OnMovement(InputValue value)
    {
        InputVector = value.Get<Vector2>();
        MoveVector.x = InputVector.x;
        MoveVector.z = InputVector.y;
    }

    private void OnRotation(InputValue value)
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
            CameraAngle -= 45f;
        else if (Keyboard.current.eKey.wasPressedThisFrame)
            CameraAngle += 45f;
    }

    private void OnHit(InputValue value)
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            playerHit = true;
    }
}