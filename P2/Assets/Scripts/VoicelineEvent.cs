using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicelineEvent : Event

{
    public string Voiceline;
    
    public GameObject Trigger;

    private void Start()
    {
        Trigger = gameObject.transform.parent.gameObject;   
    }
    public override void reaction()
    {
        //Starter den vilkårlige voiceline

        //Så skal event triggeren fjernes.
        Destroy(Trigger);
        //Til sidst skal objectet fjernes
        Destroy(gameObject);
    }
}
