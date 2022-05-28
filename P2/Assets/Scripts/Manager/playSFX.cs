using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class playSFX : MonoBehaviour
{
    [SerializeField] public AudioClip _clip;
    AudioSource audioSource;
    public AudioMixerGroup MyMixerGroup;
    public bool toggle;

    public void PlayAudio()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = _clip;
        audioSource.outputAudioMixerGroup = MyMixerGroup;
        audioSource.mute = toggle;
        if(!toggle)
        {
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
