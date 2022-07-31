using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private MainCamera mainCamera;

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private GrapplingGun grapplingGun;
    [SerializeField] private GrapplingRope rope;

    [Header("Pause Menu:")]
    [SerializeField] private MainMenu menu;
    public bool isPaused;

    private void Update(){
        isPaused = menu.transform.GetComponent<MainMenu>().isPaused;
    }

    #region Input Handler

    public void OnMove(InputAction.CallbackContext ctx){
        
        if(!rope.isGrappling && !movement._isDead && !isPaused)
            movement.Move(ctx.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !movement._isDead && !isPaused) movement.Jump();

        //if (ctx.canceled) movement.JumpCanceled();
    }

    public void OnGrappleHook(InputAction.CallbackContext ctx){
        if(!isPaused){
            if (ctx.started && !movement._isDead && grapplingGun.haveGrapple)
                grapplingGun.GrappleHook();
            else if(ctx.canceled)
                grapplingGun.GrappleHookCancel();
        }
    }

    public void OnZoomOut(InputAction.CallbackContext ctx){
        if(ctx.started && !movement._isDead && !isPaused)
            mainCamera.cameraFreeWalk.fieldOfView = mainCamera.zoomOut;
        else if(ctx.canceled && !movement._isDead && !isPaused)
            mainCamera.cameraFreeWalk.fieldOfView = mainCamera.zoomIn;
    }

    public void OnPause(InputAction.CallbackContext ctx){
        if(ctx.started && !movement._isDead)
            menu.PauseScreen();
    }

    #endregion
}
