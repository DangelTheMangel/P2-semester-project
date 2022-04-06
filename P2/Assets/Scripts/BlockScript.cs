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
        
        //remove this
        //ShieldMinigame();
        //remove this
    }


    private void Update()
    {
        if(reactionTimer > 0) //Det her tjekker om reaction timen er over 0.
        {
            Debug.Log(Time.deltaTime);
            reactionTimer -= Time.deltaTime;

            if (Random.Range(1, 100) == 1) //Det hre if-statement tjekker om du blocker
            {
                Debug.Log("U blocked it");
                GameObject.Destroy(this); //Det her stopper scripten, når du blocker.
            }
        }
        if (reactionTimer <= 0) //Hvis du bliver ramt
        {
            Debug.Log("u got hit dumbass");
        }
    } 
}
