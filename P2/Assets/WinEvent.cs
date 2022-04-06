using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEvent : Event
{
    public override void reaction()
    {
        GameManganer.Instance.winGame();
        completedReaction();
    }

    public override void completedReaction()
    {
        Destroy(gameObject);
    }
}
