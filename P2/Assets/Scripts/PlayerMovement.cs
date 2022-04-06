//based on https://youtu.be/mbzXIOKZurA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float collisionCheckerSize = .2f;
    public Transform movePoint;
    public Transform playerSprite;
    [SerializeField]
    Vector3 moveVector = new Vector3(0f, 1f, 0f);
    bool buttonRealse = true;
    private PlayerControls playerControls;

    public LayerMask whatStopsMovement;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
        GameManganer.Instance.player = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        playerControls.Freemovement.Rotate.performed += Rotate;
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
                moveVector = roatationToMovementVector(transform.eulerAngles.z / 360.0f * 2.0f * Mathf.PI);
                buttonRealse = false;
            }
            // if button was not pressed set that the button button wasnt pressed
            else if(Mathf.Abs(playerControls.Freemovement.Rotate.ReadValue<float>()) != 1f) {
                buttonRealse = true;
            }
        /*if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), collisionCheckerSize, whatStopsMovement))
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
        }*/

            if (playerControls.Freemovement.Move.triggered)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + moveVector, collisionCheckerSize, whatStopsMovement))
                {
                    movePoint.position += moveVector;
                }
            }
        }
    //UAP_AccessibilityManager.OnSwipe(ESDirection, 1);
    }

    Vector3 roatationToMovementVector(float r) {
        Debug.Log("r: " + r);
        return moveVector;
    }

}
