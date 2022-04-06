using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioClip _clipAlt;
    void Start()
    {
        SoundManager.instance.playEffect(_clip);
    }
}
