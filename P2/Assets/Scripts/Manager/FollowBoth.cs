using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoth : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //The transform of the player is searched for.
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if a player is present in the scene, the object with this script will go to the player's exact position
        if (target != null)
        {
            // sets this object's transform to the same as the player's
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
        else
        {
            // if a player isn't found, or the player is destroyed, the object with the script will be destroyed
            Destroy(gameObject);
        }
    }
}
