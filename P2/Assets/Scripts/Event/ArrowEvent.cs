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
    //whether the arrow speed is randomized
    public bool isRandom = false;
    //max and min ammounts it can take when randomized
    public float maxTime;
    public float minTime;

    [SerializeField]
    float roationdifferents = 0.3f;
    bool startRotationFound = false;
    Quaternion startRotation;


    Vector3 lowPassValue;
    float shakeDetectionThreshold = 2.0f;
    private void Start()
    {
        if (isRandom)
        {
            reactionTimer = Random.Range(minTime, maxTime);
        }

        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
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
        if (playerControls.Freemovement.Interact.triggered && reactionTimer < startplayerReaction)
        {
            return true;
        }
        else if (reactionTimer < startplayerReaction)
        {
            return (shakePhone() && reactionTimer < startplayerReaction);
        }
        else {
            return false;
        }

        
    }
    bool shakePhone() {
        if (Input.acceleration != null)
        {
            Vector3 acce = InputManage.instance.getAccelerometerVector();

            lowPassValue = Vector3.Lerp(lowPassValue, acce, InputManage.instance.getLowPassFilterFactor());
            Vector3 deltaAcceleration = acce - lowPassValue;

            if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
            {
                return true;
            }
            else {
                return false;
            }
        }
        else { 
           return false;
        }   
    }

    
    public override void faildReaction()
    {
        Debug.LogWarning("du blev ramt");
        SoundManager.instance.playEffect(GameManganer.Instance.player.gameObject, "ArrowHit");
        Destroy(arrowObject);
        Destroy(gameObject);
        GameManganer.Instance.Death();
    }

    public override void completedReaction()
    {
        SoundManager.instance.playEffect(GameManganer.Instance.player.gameObject, "ArrowBlocked");
        Debug.Log("du unvig");
        Destroy(arrowObject);
        Destroy(gameObject);
    }
}
