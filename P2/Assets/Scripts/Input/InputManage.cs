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

    /// <summary>
    /// When the object awake it make the inputmanager a singelton
    /// </summary>
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
    /// <summary>
    /// ebable the playercontroller whe nthis is enavblede
    /// </summary>
    private void OnEnable()
    {
        if (playerController != null)
            playerController.Enable();
    }
    /// <summary>
    /// Disable the player controller when this is diable
    /// </summary>
    private void OnDisable()
    {
        if (playerController != null)
            playerController.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        //make it so the when the player touch the screen the right function starts
        playerController.Touch.PrimaryContact.started += ctx => startTouchPrimary(ctx);
        playerController.Touch.PrimaryContact.canceled += ctx => endTouchPrimary(ctx);
        //calculate the lowPassFilterFactor
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        //sets the gyro to the gyroscope.attitude
        Quaternion gyro = gyroscope.attitude;
    }

    /// <summary>
    /// Return the lowpass filter factor so it can be used to decet a shake
    /// Inspred from: https://stackoverflow.com/questions/31389598/how-can-i-detect-a-shake-motion-on-a-mobile-device-using-unity3d-c-sharp
    /// </summary>
    /// <returns></returns>
    public float getLowPassFilterFactor() {
        return lowPassFilterFactor;
    }
    /// <summary>
    /// return the phones acceleration
    /// </summary>
    /// <returns></returns>
    public Vector3 getAccelerometerVector() {
        return Input.acceleration;
    }
    /// <summary>
    /// retun the phones rotation
    /// </summary>
    /// <returns></returns>
    public Quaternion getPhoneRotation()
    {
        return gyroscope.attitude;
    }
    /// <summary>
    /// This is run when the first touch on the screen end.and then calls the players movent function
    /// Inspred from: https://www.youtube.com/watch?v=XUx_QlJpd0M&t=1191s&ab_channel=samyam
    /// </summary>
    /// <param name="ctx"></param>
    private void endTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (onEndTouch != null)
        {
            onEndTouch(utilities.screenToWorld(Camera.main, playerController.Touch.PrimaryPostion.ReadValue<Vector2>()), (float)ctx.time);
            Debug.Log("endpos" + playerController.Touch.PrimaryPostion.ReadValue<Vector2>());
        }
    }
    /// <summary>
    /// This is run when the first touch on the screen starts. and then calls the players movent function
    /// Inspred from: https://www.youtube.com/watch?v=XUx_QlJpd0M&t=1191s&ab_channel=samyam
    /// </summary>
    /// <param name="ctx"></param>
    private void startTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (onStartTouch != null)
        {
            onStartTouch(utilities.screenToWorld(Camera.main, playerController.Touch.PrimaryPostion.ReadValue<Vector2>()), (float)ctx.startTime);
        }
    }
    /// <summary>
    /// This return the fingeres postion in world spaces
    /// Inspred from: https://www.youtube.com/watch?v=XUx_QlJpd0M&t=1191s&ab_channel=samyam
    /// </summary>
    /// <returns></returns>
    public Vector2 primaryPostion()
    {
        return utilities.screenToWorld(Camera.main, playerController.Touch.PrimaryPostion.ReadValue<Vector2>());
    }
    /// <summary>
    /// In Update take the mean of rotation every readDataEverFrame.
    /// </summary>
    private void Update()
    {
        if (Time.frameCount % readDataEverFrame == 0)
        {
            rotationRate = calulateRotationRateMean(rotationRateList);
            rotationRateList.Clear();
        }
        else
        {
            rotationRateList.Add(gyroscope.rotationRateUnbiased);
        }
    }
    /// <summary>
    /// This function return the mean rotation rate to other classes
    /// </summary>
    /// <returns></returns>
    public Vector3 getRotationRate() {
        return rotationRate;
    }
    /// <summary>
    /// This take the list of rotationRate and find the mean rotation rate
    /// </summary>
    /// <param name="rotationRate"></param>
    /// <returns></returns>
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
    /// <summary>
    /// This function check if the phone rotationRateValue is greater then the biases
    /// If it less thenthe bias it retunr 0. Then it chekc if the phone is rotatatede and it is possible to make an input
    /// The rotationRateValue is a float and suggestede to give the rotation rate axis 
    /// The bias is how big the roation rate should be before and input is registerede
    /// phoneRotationAxes is the axis of the phone gyro roation
    /// timer is the time it take before a new input can be rigetsrede
    /// </summary>
    /// <param name="rotationRateValue"></param>
    /// <param name="Bias"></param>
    /// <param name="phoneRotationAxes"></param>
    /// <param name="timer"></param>
    /// <returns></returns>
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
