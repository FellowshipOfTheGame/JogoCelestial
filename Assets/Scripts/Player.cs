using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    
    #region Input Handler

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement.Move(ctx.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return; // Fix to input action triggering multiple times

        movement.Jump();
    }

    #endregion
}
