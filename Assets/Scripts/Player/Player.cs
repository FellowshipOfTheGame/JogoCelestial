using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private MainCamera mainCamera;

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
        if (ctx.started && !movement._isDead && grapplingGun.haveGrapple)
            grapplingGun.GrappleHook();
        else if(ctx.canceled)
            grapplingGun.GrappleHookCancel();
        
    }

    public void OnZoomOut(InputAction.CallbackContext ctx){
        if(ctx.started)
            mainCamera.cameraFreeWalk.fieldOfView = mainCamera.zoomOut;
        else if(ctx.canceled)
            mainCamera.cameraFreeWalk.fieldOfView = mainCamera.zoomIn;
    }

    #endregion
}
