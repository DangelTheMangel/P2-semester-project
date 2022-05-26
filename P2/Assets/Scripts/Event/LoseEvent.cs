using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //Objects with this script are only spawned when the player touches an enemy
        //This script calls for the Death function in the GameManager, enemies will spawn an object with this script, giving the player a game over.
        GameManganer.Instance.Death();
        
    }


}
