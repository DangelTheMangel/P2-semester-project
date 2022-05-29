
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
/// <summary>
/// The player movement is based on https://youtu.be/mbzXIOKZurA
/// </summary>
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

    [Header("PlayerLog")]
    public Text debugDisplay;
    public bool logSwipeDetect;
    public int wallCollisionCount;
    public int inputCount;


    /// <summary>
    /// This function runs when the object awake and instaiate the playerControls and find the inputManager
    /// </summary>
    private void Awake()
    {
        inputManager = InputManage.instance;
        playerControls = new PlayerControls();
    }
    /// <summary>
    /// OnEnable run when object is enable and aktivate touch and wasd controls
    /// </summary>
    private void OnEnable()
    {
        //make so the swipe start and swipe end gets called
        inputManager.onStartTouch += swipeStart;
        inputManager.onEndTouch += swipeEnd;
        playerControls.Enable();
        //set the player in the gamemanager to this
        GameManganer.Instance.player = this;
    }
    /// <summary>
    /// OnDisable when object is diasable and it disable all controls
    /// </summary>
    private void OnDisable()
    {
        //disable touch controls
        inputManager.onStartTouch -= swipeStart;
        inputManager.onEndTouch -= swipeEnd;
        //disable wasd 
        playerControls.Disable();
    }
    /// <summary>
    /// This function run when the player touch the screen
    /// The function the time when the player start touching
    /// And the postion of the finger of the screen
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="time"></param>
    private void swipeStart(Vector2 pos, float time)
    {
        startPosition = pos;
        startTime = time;
    }

    /// <summary>
    /// This runs when the player stop touching 
    /// the screen it save the fingers postion of 
    /// the screen and time at the stop touch.
    /// it then start the detectSwipe swipe function and add an input
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="time"></param>
    private void swipeEnd(Vector2 pos, float time)
    {
        endPosition = pos;
        endTime = time;
        detectSwipe();
        inputCount++;
    }
    /// <summary>
    /// This function look at if the input is swipe and if the swipe was 
    /// long enought and took enought time. Then it take the to postion and make a vector that is udesded
    /// In the swipeDirection function
    /// </summary>
    private void detectSwipe()
    {        
        if (GameManganer.Instance.swipe && Vector3.Distance(startPosition, endPosition) >= minumumDistance &&
            (endTime - startTime) <= maximumDistance)
        {
            Debug.DrawLine(startPosition, endPosition, Color.blue, 5f);

            Vector3 dir = endPosition - startPosition;
            Vector2 dir2 = new Vector2(dir.x, dir.y).normalized;
            swipeDirection(dir2);
        }

    }
    /// <summary>
    /// This the swipe vector and figure out wich way the swipe is and make the player move that way
    /// </summary>
    /// <param name="direction"></param>
    private void swipeDirection(Vector2 direction)
    {

        if (Vector2.Dot(Vector2.up, direction) > dirThreshold)
        {
            moveplayer();
        }
        else if (Vector2.Dot(Vector2.left, direction) > dirThreshold)
        {
            rotatePlayer(-1);
        }
        else if (Vector2.Dot(Vector2.right, direction) > dirThreshold)
        {
            rotatePlayer(1);
        }
    }
    /// <summary>
    /// The rotatePlayr function rotate the player according to val. Val is expectede  to be -1 or 1.
    /// If it is -1 it turn left and 1 it turn right
    /// </summary>
    /// <param name="val"></param>
    void rotatePlayer(float val) {
        if (!isMoving) {
            transform.eulerAngles += new Vector3(0, 0, (-val * 90));
            moveVector = roatationToMovementVector(gameObject.transform.localRotation.ToEulerAngles().z);
            buttonRealse = false;
            PAC.MovementCheck();

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        PAC = GetComponent<PlayerAudioControler>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// The moveplayer moves the player.
    /// When the forward movement is activated 
    /// the player checks if the field in front of it is 
    /// not an object with the OverlapCircle() 
    /// function. The function checks if a collider 
    /// falls within a circular area and returns if it 
    /// collides with something. If there is not 
    /// anything in front of the player and the 
    /// player is not moving, the player moves 
    /// forward to where the circle was and plays 
    /// the walking sound. Otherwise it plays the 
    /// walking into a wall sound.
    /// </summary>
    void moveplayer()
    {
        if (!isMoving && !Physics2D.OverlapCircle(movePoint.position + moveVector, collisionCheckerSize, whatStopsMovement))
        {
            movePoint.position += moveVector;
            SoundManager.instance.playEffect(gameObject, footStep);
            PAC.MovementCheck();
            isMoving = true;
            //Debug.Log(isMoving);
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
        }
        if (!GameManganer.Instance.swipe) {
            detectTilde();
        }
        WASDMovementChecker();
    }

    /// <summary>
    /// This function check if the user tilt there phones.
    /// i use rotation rate and the phones rotation. It then get the input axis from the input manager.
    /// if the axis is then used in the function tildeDirection
    /// </summary>
    private void detectTilde()
    {
        //Get the rotation rate, phone roation and the tildt input axis
        Vector3 rotationRate = inputManager.getRotationRate();
        Quaternion phoneRotation = inputManager.getPhoneRotation();
        int xAxis = inputManager.getTiledAxis(rotationRate.x, bias.x, phoneRotation.x, inputManager.maxInputTimer.x);
        int yAxis = inputManager.getTiledAxis(rotationRate.y, bias.y, phoneRotation.y,inputManager.maxInputTimer.y);

        //Check if the input timer is finnished
        if (inputManager.inputTimer > 0)
        {
            inputManager.inputTimer -= Time.deltaTime;
        }
        //Check if the phone has stoppede moving
        else if(rotationRate.x < bias.x)
        {
            inputManager.isInput = false;
        }
        //send the axis
            tildeDirection(xAxis, yAxis);
    }
    /// <summary>
    /// This function take to axis. If forward axis is over 0 the player 
    /// will move forward else if the rotate axis is -1 the player turn and if it is 1 it turn right
    /// </summary>
    /// <param name="forwardAxis"></param>
    /// <param name="rotateAxis"></param>
    private void tildeDirection(int forwardAxis, int rotateAxis)
    {
        inputManager.userInput = true;
        if (forwardAxis > 0)
        {
            moveplayer();
            inputCount++;
        }else if (rotateAxis != 0)
        {
            rotatePlayer(-rotateAxis);
            inputCount++;
        }

    }
    /// <summary>
    /// Thise function check if the WASD keys have ben pressed and then move the player or turn according to the presses
    /// </summary>
    private void WASDMovementChecker()
    {
            //if the button are pressed and the button wasnt pressed last frame rotate
            if (Mathf.Abs(playerControls.Freemovement.Rotate.ReadValue<float>()) == 1f && buttonRealse)
            {
                rotatePlayer(playerControls.Freemovement.Rotate.ReadValue<float>());
                buttonRealse = false;
            }
            // if button was not pressed set that the button button wasnt pressed
            else if (Mathf.Abs(playerControls.Freemovement.Rotate.ReadValue<float>()) != 1f)
            {
                buttonRealse = true;
            }
            if (playerControls.Freemovement.Move.triggered)
            {
                moveplayer();
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
