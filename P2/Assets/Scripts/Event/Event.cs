using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    [SerializeField]
    public bool timedReaction;
    [SerializeField]
    public float reactionTimer;

    private void Start()
    {
        GameManganer.Instance.player.enabled = false;
    }

    private void OnDestroy()
    {
        GameManganer.Instance.player.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        reaction();
    }

    public virtual void reaction()
    {
        if (!timedReaction||reactionTimer > 0) //Det her tjekker om reaction timen er over 0.
        {

            reactionTimer -= Time.deltaTime;

            if (passedCheck()) //Det hre if-statement tjekker om du blocker
            {

                completedReaction(); //Det her stopper scripten, når du blocker.
            }
        }
        if (!timedReaction || reactionTimer <= 0) //Hvis du bliver ramt
        {
            faildReaction();
        }
    }

    /// <summary>
    /// checker om du har gennemført reaction 
    /// </summary>
    /// <returns></returns>
    public virtual bool passedCheck() {
        return (Random.Range(1, 100) == 1);
    } 

    /// <summary>
    /// hvad der skal ske hvis du fejler den
    /// </summary>
    public virtual void faildReaction() {
        
    }

    /// <summary>
    /// hvad der skal ske hvis du gennemføre reaction
    /// </summary>
    public virtual void completedReaction()
    {

    }


}
