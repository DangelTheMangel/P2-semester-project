//based on https://youtu.be/mbzXIOKZurA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform movePoint;
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //UAP_AccessibilityManager.OnSwipe(ESDirection, 1);
    }
}
