//based on https://youtu.be/mbzXIOKZurA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;
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

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }
        }
        //UAP_AccessibilityManager.OnSwipe(ESDirection, 1);
    }
}
