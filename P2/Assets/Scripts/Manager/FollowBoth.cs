using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoth : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
