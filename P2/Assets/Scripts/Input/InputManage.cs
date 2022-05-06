using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.Gyroscope;

/// <summary>
/// This class are inspried by https://www.youtube.com/watch?v=XUx_QlJpd0M&t=1191s&ab_channel=samyam
/// and this: https://stackoverflow.com/questions/31389598/how-can-i-detect-a-shake-motion-on-a-mobile-device-using-unity3d-c-sharp
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
    [Header("gyroscope")]
    public Gyroscope gyroscope;
    public bool gyroEnable = false;
    private PlayerController playerController;
    private Quaternion startPostion;

    [Header("accelerometer")]
    float accelerometerUpdateInterval = 1.0f / 60.0f;
    float lowPassKernelWidthInSeconds = 1.0f;
    float lowPassFilterFactor;


    [Header("tildeControls")]
    [SerializeField]
    public float readDataEverFrame = 5;
    [SerializeField]
    List<Vector3> rotationRateList = new List<Vector3>();
    [SerializeField]
    Vector3 rotationRate = Vector3.zero;
    [SerializeField]
    public Vector2 maxInputTimer = new Vector2(0.5f,0.7f);
    [SerializeField]
    public float inputTimer = 0.5f;
    [SerializeField]
    public bool isInput = false;
    public bool userInput = true;

    public bool mOverm = true;


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
        if (playerController != null)
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
        //
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;

        Quaternion gyro = gyroscope.attitude;
        startPostion = new Quaternion(gyro.x, gyro.y, gyro.z, gyro.w);
    }

    public float getLowPassFilterFactor() {
        return lowPassFilterFactor;
    }
    public Vector3 getAccelerometerVector() {
        return Input.acceleration;
    }

    public Quaternion getPhoneRotation()
    {
        return gyroscope.attitude;
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

    private void Update()
    {
        if (Time.frameCount % readDataEverFrame == 0)
        {
            if (mOverm)
                rotationRate = calulateRotationRateMean(rotationRateList);
            else
                rotationRate = rotationRateList[(int)(rotationRateList.Count-1)];
            rotationRateList.Clear();
        }
        else
        {
            rotationRateList.Add(gyroscope.rotationRateUnbiased);
        }
    }

    public Vector3 getRotationRate() {
        return rotationRate;
    }

    public Vector3 calulateRotationRateMean(List<Vector3> rotationRate)
    {
        Vector3 meanVector = Vector3.zero;
        for (int i = 0; i < rotationRate.Count; i++)
        {
            meanVector = new Vector3(meanVector.x + rotationRate[i].x, meanVector.y + rotationRate[i].y, meanVector.z + rotationRate[i].z);
        }
        meanVector = new Vector3((float)meanVector.x / rotationRate.Count, meanVector.y / rotationRate.Count, meanVector.z / rotationRate.Count);
        return meanVector;
    }

    public int getTiledAxis(float rotationRateValue, float Bias, float phoneRotationAxes , float timer)
    {
        if (rotationRateValue < Bias && rotationRateValue > -Bias)
        {
            return 0;

        }
        else
        {
            if (rotationRateValue > Bias && !isInput && phoneRotationAxes < 0 && inputTimer <= 0)
            {
                isInput = true;
                userInput = false;
                inputTimer = timer;
                return 1;
            }
            if (rotationRateValue > Bias && !isInput && phoneRotationAxes > 0 && inputTimer <= 0)
            {
                isInput = true;
                userInput = false;
                inputTimer = timer;
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
