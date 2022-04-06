using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEvent : Event
{
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
        Destroy(gameObject);

    }

    public override void completedReaction()
    {
        Destroy(gameObject);
    }
}
