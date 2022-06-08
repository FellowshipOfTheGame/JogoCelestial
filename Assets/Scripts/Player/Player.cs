using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private GrapplingGun grapplingGun;
    [SerializeField] private GrapplingRope rope;

    private void Update()
    {
    }

    #region Input Handler

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if(!rope.isGrappling)
            movement.Move(ctx.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) movement.Jump();

        //if (ctx.canceled) movement.JumpCanceled();
    }

    public void OnGrappleHook(InputAction.CallbackContext ctx){
        if (ctx.started)
            grapplingGun.GrappleHook();
        else if(ctx.canceled)
            grapplingGun.GrappleHookCancel();
        
    }

    #endregion
}
