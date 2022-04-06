using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEvent : Event
{
    public float startReaction;
    public GameObject arrowObject;
    public GameObject arrowprefab;
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
    public override void reaction()
    {
        Debug.Log("igang: " + reactionTimer);
        base.reaction();
    }

    public override bool passedCheck()
    {
        return (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f);
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
