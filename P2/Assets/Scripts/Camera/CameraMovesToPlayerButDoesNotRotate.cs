using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovesToPlayerButDoesNotRotate : MonoBehaviour
{
    GameObject Gamer;
    Vector3 offset = new Vector3(0, 0, -10);

    // Start is called before the first frame update
    void Start()
    {
        //finds the player
        Gamer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //move the Cameras transform to the players transform and add offset
        transform.position = Gamer.transform.position + offset;
    }
}
