//based on https://youtu.be/mbzXIOKZurA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float collisionCheckerSize = .2f;
    public Transform movePoint;
    public Transform playerSprite;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    /// <summary>
    /// Checks when horizontal or vertical input axis is 1 or -1. If true, it will move towards <see cref="movePoint"/>
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(playerControls.Freemovement.Rotate.ReadValue<float>()) == 1f)
            {
                Debug.Log(playerControls.Freemovement.Rotate.ReadValue<float>());
                transform.eulerAngles += new Vector3(0, 0, (playerControls.Freemovement.Rotate.ReadValue<float>() * 90));
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
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), collisionCheckerSize, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, 1f, 0f);
                }
            }
            
        }
        //UAP_AccessibilityManager.OnSwipe(ESDirection, 1);
    }
}
