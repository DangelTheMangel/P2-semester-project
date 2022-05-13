using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{

    public string level = "Level1";
    public void startTheGame() {
        GameManganer.Instance.sceneManganer.LoadLevel(level);
    }
}
