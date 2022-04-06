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
        
        endPos = GameManganer.Instance.player.transform;
    }
    private void FixedUpdate()
    {
        float arrowTime = 1- (arrowEvent.reactionTimer / arrowEvent.startReaction);

        if (arrowTime < 0.5f)
        Debug.Log(arrowTime);
        else
        Debug.LogWarning(arrowTime);
        transform.position = Vector3.Lerp(startPos.position, endPos.position, arrowTime);
    }
}
