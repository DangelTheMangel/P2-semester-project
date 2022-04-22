using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEnabler : MonoBehaviour
{
    public AudioSource mainAudio;
    public AudioClip audioOn;
    public AudioClip audioOff;
    public bool vocielineOn = true;
    [SerializeField]
    string[] buttonText = { "Vocieline On", "Vocieline Off" };
    [SerializeField]
    Text buttonTextObj;

    // Start is called before the first frame update
    void Start()
    {
        turnOn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerPrefs.GetInt("DialogueOff") == 1)
            {
                vocielineOn = true;
                turnOn();
                buttonTextObj.text = buttonText[0];
            }
            else if (PlayerPrefs.GetInt("DialogueOff") == 0)
            {
                vocielineOn = false;
                turnOff();
                buttonTextObj.text = buttonText[1];
            }

        }
    }

    public void toogleVocieline() {
        if (vocielineOn)
        {
            vocielineOn = false;
            turnOff();
            buttonTextObj.text = buttonText[1];

        }
        else {
            vocielineOn = true;
            turnOn();
            buttonTextObj.text = buttonText[0];
        }  
    }
    public void turnOff()
    {
        PlayerPrefs.SetInt("DialogueOff", 1);
        mainAudio.PlayOneShot(audioOff, 1);
    }
    public void turnOn()
    {
        PlayerPrefs.SetInt("DialogueOff", 0);
        mainAudio.PlayOneShot(audioOn, 1);
    }
}
