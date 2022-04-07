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
    [SerializeField] private AudioSource musicSource, effectSource, voiceSource;
    [SerializeField] private GameObject preSFX;
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
    public void playEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void playEffect(GameObject target, AudioClip clip)
    {
        GameObject obj = Instantiate(preSFX, target.transform.position, target.transform.rotation, target.transform);

        obj.GetComponent<playSFX>()._clip = clip;
    }

    public void playVoice(AudioClip vclip)
    {
        voiceSource.PlayOneShot(vclip);
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

public class AudioSoundClip
{
    public string sound_name;
    public AudioClip audio;
}