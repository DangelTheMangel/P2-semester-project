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
    [SerializeField] private bool toggle;
    
    [SerializeField] private VoiceLines[] VoiceArray;
    [SerializeField] private GameObject VoiceStart;
    public void Awake()
    {
        //tjekker om der er en instance og hvis der ikke er
        //g�re denne til instance ellers �dl�g dette gameobject
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

    public void InstantiateVoice(AudioClip vclip)
    {
        effectSource.PlayOneShot(vclip);
    }

    public void InstantiateVoice(GameObject target, string VoiceName)
    {
        GameObject voi = Instantiate(VoiceStart, target.transform.position, target.transform.rotation, target.transform);

        voi.GetComponent<PlayVoice>()._voices = findSound(VoiceName).audio;
        
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

[Serializable]

public class VoiceLines
{
    public string voice_name;
    public AudioClip audio;
}