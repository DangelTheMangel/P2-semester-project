using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEvent : Event
{
    //the value of the reaction
    public float startReaction;
    //the time when the play needs inputs
    public float inputstartReaction;
    //the arrows gameobject
    public GameObject arrowObject;
    //the prefab use for the arrow
    public GameObject arrowprefab;
    //the name of the  waypoint
    public string arrowpointName;
    private void Start()
    {
        startReaction = reactionTimer;
        //loop through all the object parent childs and find arrowpoint
        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.name == arrowpointName) {
                arrowObject = Instantiate(arrowprefab, child.transform);
                arrowObject.transform.parent = gameObject.transform.parent;
                arrowObject.GetComponent<arrowObject>().arrowEvent = this;
                arrowObject.GetComponent<arrowObject>().startPos = child.transform;
                break;
            }
        }
    }
    //start the event
    public override void reaction()
    {
        Debug.Log("igang: " + reactionTimer);
        base.reaction();
    }

    //check if you press to move to the site
    public override bool passedCheck()
    {

        return (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && reactionTimer < inputstartReaction);
    }

    //for now destory the object and arrow
    public override void faildReaction()
    {
        Debug.LogWarning("du blev ramt");
        Destroy(arrowObject);
        Destroy(gameObject);
    }

    //for now destory the object and arrow
    public override void completedReaction()
    {
        Debug.Log("du unvig");
        Destroy(arrowObject);
        Destroy(gameObject);
    }
}
