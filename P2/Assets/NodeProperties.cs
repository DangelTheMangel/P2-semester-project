using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeProperties : MonoBehaviour
{
    public GameObject[] Nodes;
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
        }

    }
}
