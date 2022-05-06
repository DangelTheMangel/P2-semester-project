using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManganer : MonoBehaviour
{
    public static GameManganer Instance;
    public GameObject gameManagerPrefab;
    public SceneManganer sceneManganer;
    public int sceneOfDeath;
    public bool shake = false;
    public bool swipe = false;
    public PlayerMovement player;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Death()
    {
        sceneOfDeath = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("GameOver");
        Debug.Log("Get Owned Loser");
    }

  

    private void Update()
    {
        if (player == null && SceneManager.GetActiveScene().name != "GameOver" && SceneManager.GetActiveScene().buildIndex != 0)
        {
            player = FindObjectOfType<PlayerMovement>();
        }
    }

    public void winGame() {
        Debug.Log("game won ");
        if (sceneManganer == null) { 
            sceneManganer = FindObjectOfType<SceneManganer>();
        }
        if (sceneManganer != null) {
            sceneManganer.loadNextLevel();
            }
        else{
                Debug.LogWarning("et eller andet er galt");
            }
        
    }
}
