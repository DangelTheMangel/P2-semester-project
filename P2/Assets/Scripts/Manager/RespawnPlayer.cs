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
        //debug command, when testing on PC, space resets the game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(GameManganer.Instance.sceneOfDeath);
        }
    }

    //This function sends the player back to the scene where they died.
    public void Resetter()
    {
        //The "sceneOfDeath" int from the gameManager is used as the scene number.
        //This int was previously set to be the number of the scene where the player died.
        SceneManager.LoadScene(GameManganer.Instance.sceneOfDeath);
    }
}
