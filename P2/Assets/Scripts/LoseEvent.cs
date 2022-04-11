using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SoundManager.instance.playEffect(GameManganer.Instance.player.gameObject, "ArrowHit");
        GameManganer.Instance.Death();
        
    }


}
