using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSFX : MonoBehaviour
{
    [SerializeField] public AudioClip _clip;
    AudioSource audioSource;
    void Start()
    {
        SoundManager.instance.playEffect(_clip);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = _clip;
    }

    private void LateUpdate()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
