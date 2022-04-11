using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEnabler : MonoBehaviour
{
    public AudioSource mainAudio;
    public AudioClip audioOn;
    public AudioClip audioOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerPrefs.GetInt("DialogueOff") == 1)
            {
                turnOn();
            }
            else if (PlayerPrefs.GetInt("DialogueOff") == 0)
            {
                turnOff();
            }

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
