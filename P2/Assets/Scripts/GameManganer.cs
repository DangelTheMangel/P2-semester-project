using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManganer : MonoBehaviour
{
    public static GameManganer Instance;
    public SceneManganer sceneManganer;

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

    public PlayerMovement player;

    public void winGame() {
        Debug.Log("game won ");
        sceneManganer.loadScene(0);
    }
}
