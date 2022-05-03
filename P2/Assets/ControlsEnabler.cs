using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsEnabler : MonoBehaviour
{
    public bool shakeOn = true;
    [SerializeField]
    string[] buttonText = { "Shake On", "Tilted On" };
    [SerializeField]
    Text buttonTextObj;
    [SerializeField]
    GameObject GameManagerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        turnOn();
    }


    public void tooglecontrols()
    {
        if (shakeOn)
        {
            shakeOn = false;
            turnOff();
            buttonTextObj.text = buttonText[1];

        }
        else
        {
            shakeOn = true;
            turnOn();
            buttonTextObj.text = buttonText[0];
        }
    }
    public void turnOff()
    {
        GameManagerPrefab.GetComponent<GameManganer>().swipe = false;
    }
    public void turnOn()
    {
        GameManagerPrefab.GetComponent<GameManganer>().swipe = true;
    }
}
