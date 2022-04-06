using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class controlls all the sound and are inspired by :
/// https://www.youtube.com/watch?v=tEsuLTpz_DU&ab_channel=Tarodev
/// https://www.youtube.com/watch?v=DcbxFugk5pM&ab_channel=CodeRadiance
/// 
/// Should implement this:
/// https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource musicSource, effectSource;
    public void Awake()
    {
        //tjekker om der er en instance og hvis der ikke er
        //gøre denne til instance ellers ødlæg dette gameobject
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Play a sfx as the audioClip
    /// </summary>
    /// <param name="audioClip"></param>
    public void playEffect(AudioClip audioClip)
    {
        effectSource.PlayOneShot(audioClip);
    }

    public void changeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void changeMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void changeEffectVolume(float value)
    {
        effectSource.volume = value;
    }

    public void toggleEffect()
    {
        effectSource.mute = !effectSource.mute;
    }

    public void toggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

}