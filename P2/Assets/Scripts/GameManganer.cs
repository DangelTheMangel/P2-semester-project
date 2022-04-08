using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManganer : MonoBehaviour
{
    public static GameManganer Instance;
    public SceneManganer sceneManganer;
    public int sceneOfDeath;

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

    public PlayerMovement player;

    private void Update()
    {
        if (player == null && SceneManager.GetActiveScene().name != "GameOver")
        {
            player = FindObjectOfType<PlayerMovement>();
        }
    }

    public void winGame() {
        Debug.Log("game won ");
        sceneManganer.loadNextLevel();
    }
}
