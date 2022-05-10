//based on https://youtu.be/mbzXIOKZurA

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("input")]
    [SerializeField]
    private float minumumDistance = .2f;
    [SerializeField]
    private float maximumDistance = 1;
    [SerializeField, Range(0, 1)]
    private float dirThreshold = .9f;

    private InputManage inputManager;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    [Header("player settings")]
    public float moveSpeed = 100f;
    public float collisionCheckerSize = .2f;
    public Transform movePoint;
    public Transform playerSprite;
    [SerializeField]
    Vector3 moveVector = new Vector3(0f, 1f, 0f);
    bool buttonRealse = true;
    private PlayerControls playerControls;
    public PlayerAudioControler PAC;
    public LayerMask whatStopsMovement;
    [SerializeField] private string turnSound = "TurnSound";
    [SerializeField] private string footStep = "Footstep";
    [SerializeField] private string hitWall = "HitWall";
    [SerializeField] bool isMoving = false;
    Rigidbody2D rigidBody;
    [Header("tildeControls")]
    [SerializeField]
    Vector3 bias;

    [Header("Debug stuff")]
    public Text debugDisplay;
    public int wallCollisionCount;
    public int swipesCount;



    private void Awake()
    {
        //new
        inputManager = InputManage.instance;
        //old
        inputManager = InputManage.instance;
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        //new
        inputManager.onStartTouch += swipeStart;
        inputManager.onEndTouch += swipeEnd;
        //
        playerControls.Enable();
    }
    private void OnDisable()
    {
        //new
        inputManager.onStartTouch -= swipeStart;
        inputManager.onEndTouch -= swipeEnd;
        //
        playerControls.Disable();
        GameManganer.Instance.player = this;
    }

    private void swipeStart(Vector2 pos, float time)
    {
        startPosition = pos;
        startTime = time;
    }

    private void swipeEnd(Vector2 pos, float time)
    {
        endPosition = pos;
        endTime = time;
        detectSwipe();
    }

    private void detectSwipe()
    {
        Debug.LogWarning("detectSwipe " + (Vector3.Distance(startPosition, endPosition) >= minumumDistance &&
            (endTime - startTime) <= maximumDistance) + " swipe on: " + GameManganer.Instance.swipe);
        if (GameManganer.Instance.swipe && Vector3.Distance(startPosition, endPosition) >= minumumDistance &&
            (endTime - startTime) <= maximumDistance)
        {
            Debug.DrawLine(startPosition, endPosition, Color.blue, 5f);

            Vector3 dir = endPosition - startPosition;
            Vector2 dir2 = new Vector2(dir.x, dir.y).normalized;
            Debug.Log("dir:" + dir + "dir2" + dir2 + "s" + startPosition + "e" + endPosition);
            swipeDirection(dir2);
        }

    }

    private void swipeDirection(Vector2 direction)
    {

        if (Vector2.Dot(Vector2.up, direction) > dirThreshold)
        {
            Debug.Log("swipeUP");
            moveplayer();
        }
        else if (Vector2.Dot(Vector2.down, direction) > dirThreshold)
        {
            Debug.Log("swipeDown");
        }
        else if (Vector2.Dot(Vector2.left, direction) > dirThreshold)
        {
            Debug.Log("swipeLeft");
            rotatePlayer(-1);
        }
        else if (Vector2.Dot(Vector2.right, direction) > dirThreshold)
        {
            Debug.Log("swipeRight");
            rotatePlayer(1);
        }
    }

    void rotatePlayer(float val) {
        if (!isMoving) {
            Debug.Log(playerControls.Freemovement.Rotate.ReadValue<float>());
            transform.eulerAngles += new Vector3(0, 0, (-val * 90));
            moveVector = roatationToMovementVector(gameObject.transform.localRotation.ToEulerAngles().z);
            buttonRealse = false;
            PAC.MovementCheck();

        }
        else
        {
            SoundManager.instance.playEffect(gameObject, hitWall);
            wallCollisionCount++;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        playerControls.Freemovement.Rotate.performed += Rotate;
        PAC = GetComponent<PlayerAudioControler>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Rotate(InputAction.CallbackContext context)
    {
        Debug.Log("Rotate");
    }
    /*
    private IEnumerable CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 finalPos = transform.position;

        if (startPos.x != finalPos.x || startPos.y != finalPos.y || startPos.z != finalPos.z)
            isMoving = true;
        Debug.Log(isMoving);
    }
    */

    void moveplayer()
    {
        if (!isMoving && !Physics2D.OverlapCircle(movePoint.position + moveVector, collisionCheckerSize, whatStopsMovement))
        {
            movePoint.position += moveVector;
            SoundManager.instance.playEffect(gameObject, footStep);
            PAC.MovementCheck();
            isMoving = true;
            Debug.Log(isMoving);
        }
        else if (!isMoving)
        {
            SoundManager.instance.playEffect(gameObject, hitWall);
            wallCollisionCount++;
        }
    }

    /// <summary>
    /// Checks when horizontal or vertical input axis is 1 or -1. If true, it will move towards <see cref="movePoint"/>
    /// </summary>
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (transform.position == movePoint.position)
        {
            PAC.MovementCheck();
            isMoving = false;
            Debug.Log(isMoving);
        }
        if (!GameManganer.Instance.swipe) {
            detectTilde();
        }
        WASDMovementChecker();
        updateDebugText();
    }

    private void detectTilde()
    {
        updateDebugText();
        Vector3 rotationRate = inputManager.getRotationRate();
        Quaternion phoneRotation = inputManager.getPhoneRotation();
        int xAxis = inputManager.getTiledAxis(rotationRate.x, bias.x, phoneRotation.x, inputManager.maxInputTimer.x);
        int yAxis = inputManager.getTiledAxis(rotationRate.y, bias.y, phoneRotation.y,inputManager.maxInputTimer.y);

        if (inputManager.inputTimer > 0)
        {
            inputManager.inputTimer -= Time.deltaTime;
        }
        else if(rotationRate.x < bias.x)
        {
            inputManager.isInput = false;
        }

        
            tildeDirection(xAxis, yAxis);
        
    }
    public void updateDebugText() {
        if (debugDisplay != null) {
            debugDisplay.text = "ismoving: " + isMoving;
        }    

    }


    /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// //////

    public void ChangeBiasX(float value)
    {
        bias.x = value;
    }

    public void ChangeBiasY(float value)
    {
        bias.y = value;
    }

    public void ChangeTimerX(float value)
    {
        inputManager.maxInputTimer.x = value;
    }

    public void ChangeTimerY(float value)
    {
        inputManager.maxInputTimer.y = value;
    }
    /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// ////// /// //////

    private void tildeDirection(int forwardAxis, int rotateAxis)
    {
        inputManager.userInput = true;
        if (forwardAxis > 0)
        {
            moveplayer();
        }else if (rotateAxis != 0)
        {
            rotatePlayer(-rotateAxis);
        }

    }

    private void WASDMovementChecker()
    {
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            //if the button are pressed and the button wasnt pressed last frame rotate
            if (Mathf.Abs(playerControls.Freemovement.Rotate.ReadValue<float>()) == 1f && buttonRealse)
            {
                Debug.Log(playerControls.Freemovement.Rotate.ReadValue<float>());
                transform.eulerAngles += new Vector3(0, 0, (-playerControls.Freemovement.Rotate.ReadValue<float>() * 90));
                moveVector = roatationToMovementVector(gameObject.transform.localRotation.ToEulerAngles().z);
                buttonRealse = false;
                PAC.MovementCheck();
                SoundManager.instance.playEffect(gameObject, turnSound);
            }
            // if button was not pressed set that the button button wasnt pressed
            else if (Mathf.Abs(playerControls.Freemovement.Rotate.ReadValue<float>()) != 1f)
            {
                buttonRealse = true;
            }
            if (playerControls.Freemovement.Move.triggered)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + moveVector, collisionCheckerSize, whatStopsMovement))
                {
                    movePoint.position += moveVector;
                    SoundManager.instance.playEffect(gameObject, footStep);
                }
                else
                {
                    SoundManager.instance.playEffect(gameObject, hitWall);
                }
            }
        }
    }
    
    


    /// <summary>
    /// rotate radiant angle (+ PI/2) and convert to movement 
    /// vector 
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public Vector3 roatationToMovementVector(float r) {
        //this is done with unit circle
        Vector3 vector = new Vector3(Mathf.Round(Mathf.Cos(Mathf.PI / 2 + r)),Mathf.Round(Mathf.Sin(Mathf.PI/2 + r)),0);

        return vector;
    }

}
