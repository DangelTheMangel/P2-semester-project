using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoiceLevel1 : MonoBehaviour
{
    public GameObject existingMusic;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetInt("DialogueOff") == 1)
        {
            Destroy(gameObject);
        }
        existingMusic = FindObjectOfType<VoiceLevel1>().gameObject;
        if (existingMusic != null & existingMusic != this.gameObject)
        {
            Destroy(gameObject);
        }
        Object.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject);
        }
    }
}
