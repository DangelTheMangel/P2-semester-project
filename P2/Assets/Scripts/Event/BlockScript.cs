using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    float reactionTimer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ShieldMinigme lmao");
        
        //Attack flyver efter dig
        reactionTimer = 3;
    }
}
