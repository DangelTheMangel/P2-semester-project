using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void Playgame()
    {
        //Change Scene when pressing start
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Or one where the name of the level is used to load (just remove //)
        //SceneManager.LoadScene("Test Level") ; 
    }
    public void QuitGame ()
    {

        // Close the game when pressing exit

        //Display in log that the game is quitting

        Debug.Log("Quit");
        Application.Quit();
    }
}
