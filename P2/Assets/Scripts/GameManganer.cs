using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManganer : MonoBehaviour
{
    public static GameManganer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public PlayerMovement player;

    public void winGame() {
        Debug.Log("game won ");
    }
}
