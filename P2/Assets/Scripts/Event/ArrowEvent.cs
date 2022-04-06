using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEvent : Event
{
    //the start of the event
    public float startReaction;
    //the start of the players reaction
    public float startplayerReaction = 3;
    //the arrow object
    public GameObject arrowObject;
    //the arrow prefab
    public GameObject arrowprefab;
    //the name of the arrow point object
    public string arrowpointName;
    //the input system class
    private PlayerControls playerControls;
    private void Start()
    {
        //the inputsystem class begin intilsiset
        playerControls = new PlayerControls();
        //Input enabled
        playerControls.Enable();

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
    public override void reaction()
    {
        Debug.Log("igang: " + reactionTimer);
        base.reaction();
    }

    public override bool passedCheck()
    {
        return (playerControls.Freemovement.Interact.triggered && reactionTimer < startplayerReaction);
    }

    public override void faildReaction()
    {
        Debug.LogWarning("du blev ramt");
        Destroy(arrowObject);
        Destroy(gameObject);
    }

    public override void completedReaction()
    {
        Debug.Log("du unvig");
        Destroy(arrowObject);
        Destroy(gameObject);
    }
}
