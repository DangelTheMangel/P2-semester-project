//based on https://youtu.be/mbzXIOKZurA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        Debug.Log("detectSwipe" + (Vector3.Distance(startPosition, endPosition) >= minumumDistance &&
            (endTime - startTime) <= maximumDistance));
        if (Vector3.Distance(startPosition, endPosition) >= minumumDistance &&
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
        Debug.Log(playerControls.Freemovement.Rotate.ReadValue<float>());
        transform.eulerAngles += new Vector3(0, 0, (-val * 90));
        moveVector = roatationToMovementVector(gameObject.transform.localRotation.ToEulerAngles().z);
        buttonRealse = false;
        PAC.MovementCheck();
        SoundManager.instance.playEffect(gameObject, turnSound);
    }

    void moveplayer() {
        if (!Physics2D.OverlapCircle(movePoint.position + moveVector, collisionCheckerSize, whatStopsMovement))
        {
            movePoint.position += moveVector;
            SoundManager.instance.playEffect(gameObject, footStep);
            PAC.MovementCheck();
        }
        else
        {
            SoundManager.instance.playEffect(gameObject, hitWall);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        playerControls.Freemovement.Rotate.performed += Rotate;
        PAC = GetComponent<PlayerAudioControler>();
    }
    private void Rotate(InputAction.CallbackContext context)
    {
        Debug.Log("Rotate");
    }

    /// <summary>
    /// Checks when horizontal or vertical input axis is 1 or -1. If true, it will move towards <see cref="movePoint"/>
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

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
            else if(Mathf.Abs(playerControls.Freemovement.Rotate.ReadValue<float>()) != 1f) {
                buttonRealse = true;
            }

            if (playerControls.Freemovement.Move.triggered)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + moveVector, collisionCheckerSize, whatStopsMovement))
                {
                    movePoint.position += moveVector;
                    SoundManager.instance.playEffect(gameObject, footStep);
                    PAC.MovementCheck();
                }
                else
                {
                    SoundManager.instance.playEffect(gameObject, hitWall);
                }
            }
        }
    //UAP_AccessibilityManager.OnSwipe(ESDirection, 1);
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
