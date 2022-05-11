using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour 

{
  PauseAction action;

  public static bool PausedGame = false;
  public GameObject pauseMenuUI; 
  
  
 private void Awake()
 {
     action = new PauseAction();
    
 }

 private void OnEnable() {
    action.Enable();
  }

  private void OnDisable(){
      action.Disable() ;
  }
 
 

    private void Start() 
    {
              //Checks if the game is paused or not
       action.Pause.PauseGame.performed += _ => DeterminePause(); 
     }
        
        private void DeterminePause()
        {
        
            if (PausedGame)
            {
                Resume();    
            } else 
            {
                Pause();
            }
        }
     

      //Written with help from https://www.youtube.com/watch?v=JivuXdrIHK0&t=482s
     //when Paused is clicked(escape) The Pause UI shows, the Sounds is muted and time stops
 public void Pause()

 {
 pauseMenuUI.SetActive(true);
 Time.timeScale = 0f;
  PausedGame = true;
   AudioListener.pause = true;
   
 }

       //When resume is clicked the time go back to normal and the sound and time starts again
   public void Resume()

    {
  pauseMenuUI.SetActive(false);
 Time.timeScale = 1f;
 PausedGame = false ;
 AudioListener.pause = false;
 
 

    }

 
 //When MainMenu Button is clicked the time goes back to normal and the goes to Main menu
 public void MainMenu()

 {
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainMenu");
 
 }
 // When the end button is clicked the application ends
    public void EndGame()
    
    {

 Application.Quit();
    }
}
