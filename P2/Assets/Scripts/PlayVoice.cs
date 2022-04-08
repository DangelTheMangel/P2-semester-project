using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVoice : MonoBehaviour

   
{
    [SerializeField] public AudioClip _voices;
    AudioSource voiceLines;

       void Start()
    {
        SoundManager.instance.playVoice(_voices);
        voiceLines = GetComponent<AudioSource>();
        voiceLines.clip = _voices;
    }

    private void LateUpdate()
    {
        if (!voiceLines.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
