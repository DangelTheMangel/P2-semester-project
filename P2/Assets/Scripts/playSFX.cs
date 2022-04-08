using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class playSFX : MonoBehaviour
{
    [SerializeField] public AudioClip _clip;
    AudioSource audioSource;
    public AudioMixerGroup MyMixerGroup;
    void Start()
    {
        SoundManager.instance.playEffect(_clip);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = _clip;
        audioSource.outputAudioMixerGroup = MyMixerGroup;
    }

    private void LateUpdate()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
