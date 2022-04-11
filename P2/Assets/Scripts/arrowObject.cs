using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowObject : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public ArrowEvent arrowEvent;

    private void Awake()
    {
        //sets end postion to the players postion
        endPos = GameManganer.Instance.player.transform;
    }
    private void FixedUpdate()
    {
        //calculate how long the have have reacts
        float arrowTime = 1- (arrowEvent.reactionTimer / arrowEvent.startReaction);
        //change the arrows potion bewteen to points
        transform.position = Vector3.Lerp(startPos.position, endPos.position, arrowTime);
    }
}
