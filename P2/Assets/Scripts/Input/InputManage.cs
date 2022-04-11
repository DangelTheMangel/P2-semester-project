using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.Gyroscope;

/// <summary>
/// This class are inspried by https://www.youtube.com/watch?v=XUx_QlJpd0M&t=1191s&ab_channel=samyam
/// </summary>
[DefaultExecutionOrder(-1)]
public class InputManage : MonoBehaviour
{
    public static InputManage instance;
    #region Events
    public delegate void startTouch(Vector2 postion, float time);
    public event startTouch onStartTouch;

    public delegate void endTouch(Vector2 postion, float time);
    public event endTouch onEndTouch;
    #endregion
    public Gyroscope gyroscope;
    public bool gyroEnable = false;
    private PlayerController playerController;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        gyroscope = Input.gyro;
        if (gyroscope != null) {
            gyroscope.enabled = true;
            gyroEnable = gyroscope.enabled;
        }
        playerController = new PlayerController();
    }

    private void OnEnable()
    {
        if(playerController != null)
        playerController.Enable();
    }

    private void OnDisable()
    {
        if (playerController != null)
            playerController.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerController.Touch.PrimaryContact.started += ctx => startTouchPrimary(ctx);
        playerController.Touch.PrimaryContact.canceled += ctx => endTouchPrimary(ctx);
    }

    private void endTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (onEndTouch != null)
        {
            onEndTouch(utilities.screenToWorld(Camera.main, playerController.Touch.PrimaryPostion.ReadValue<Vector2>()), (float)ctx.time);
            Debug.Log("endpos" + playerController.Touch.PrimaryPostion.ReadValue<Vector2>());
        }
    }

    private void startTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (onStartTouch != null)
        {
            onStartTouch(utilities.screenToWorld(Camera.main, playerController.Touch.PrimaryPostion.ReadValue<Vector2>()), (float)ctx.startTime);
            Debug.Log("startpos" + playerController.Touch.PrimaryPostion.ReadValue<Vector2>());
        }
    }

    public Vector2 primaryPostion()
    {
        return utilities.screenToWorld(Camera.main, playerController.Touch.PrimaryPostion.ReadValue<Vector2>());
    }


    // Update is called once per frame
    void Update()
    {

    }
}
