using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private GrapplingGun grapplingGun;

    private void Update()
    {
        grapplingGun.UpdateTargetPosition(mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    #region Input Handler

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement.Move(ctx.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) movement.Jump();

        if (ctx.canceled) movement.JumpCanceled();
    }

    public void OnGrappleHook(InputAction.CallbackContext ctx)
    {
        if (ctx.started) grapplingGun.Shoot();

        if (ctx.canceled) grapplingGun.Release();
    }

    #endregion
}
