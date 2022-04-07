using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovesToPlayerButDoesNotRotate : MonoBehaviour
{
    GameObject Gamer;

    // Start is called before the first frame update
    void Start()
    {
        Gamer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Gamer.transform.position + new Vector3(0, 0, -10);
    }
}
