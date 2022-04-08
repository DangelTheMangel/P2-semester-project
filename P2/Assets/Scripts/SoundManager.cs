using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
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
    [SerializeField] private AudioSoundClip[] SoundArray;
    
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

    public void playEffect(GameObject target, string soundName)
    {
        GameObject obj = Instantiate(preSFX, target.transform.position, target.transform.rotation, target.transform);

        obj.GetComponent<playSFX>()._clip = findSound(soundName).audio;

        obj.GetComponent<playSFX>().MyMixerGroup = findSound(soundName).mixerGroup;
        obj.GetComponent<playSFX>().PlayAudio();
    }

    public void playVoice(AudioClip vclip)
    {
        voiceSource.PlayOneShot(vclip);
    }

    public void changeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public AudioSoundClip findSound(string audioName)
    {
        AudioSoundClip selected = null;
        
        for(int i = 0; i<SoundArray.Length; ++i)
        {
            AudioSoundClip currentI = SoundArray[i];
            if (audioName == currentI.sound_name)
            {
                selected = currentI;
                break;
            }
        }

        if(selected == null)
        {
            Debug.LogError("Sound Cringe");
            return null;
        }
        return selected;
    }

}

[Serializable]
public class AudioSoundClip
{
    public string sound_name;
    public AudioClip audio;
    public AudioMixerGroup mixerGroup;
}