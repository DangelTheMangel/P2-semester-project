using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(GameManganer.Instance.sceneOfDeath);
        }
    }

    public void Resetter()
    {
        SceneManager.LoadScene(GameManganer.Instance.sceneOfDeath);
    }
}