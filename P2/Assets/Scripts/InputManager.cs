using UnityEngine;
using UnityEngine.InputSystem;

//based on https://youtu.be/XUx_QlJpd0M

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{

    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 postion, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private PlayerControls playerControls;
    private Camera mainCamera;


    private void Awake()
    {
        playerControls = new PlayerControls();
        mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    /// <summary>
    /// Subscribes to input action touch events
    /// </summary>
    void Start()
    {
        playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    /// <summary>
    /// Input System
    /// Called when the touch action starts
    /// </summary>
    /// <param name="context">Information regarding the event, you can read the value of the touch</param>
    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }
    /// <summary>
    /// Returns position of the finger, for the trail renderer.
    /// </summary>
    /// <returns></returns>
    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
