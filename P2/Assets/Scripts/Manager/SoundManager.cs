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
    
    [SerializeField] private VoiceLines[] VoiceArray;
    [SerializeField] private GameObject VoiceStart;

    public void Awake()
    {
        ///<summary>
        ///Checks if there is a SoundManager in scene, if there is and intance it destroys the old one and makes a new.
        ///</summary>
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

    /// <summary>
    /// playEffect instantiates an object that contains the audio sorce.
    /// the correct sound is found thourgh and array and is played.
    /// This also adds the audio toggle and the mixer to each element.
    /// </summary>
    public void playEffect(GameObject target, string soundName)
    {
        GameObject obj = Instantiate(preSFX, target.transform.position, target.transform.rotation, target.transform);

        AudioSoundClip aSC = findSound(soundName);
        playSFX sFX = obj.GetComponent<playSFX>();
        sFX._clip = aSC.audio;
        sFX.toggle = aSC.toggle;
        sFX.MyMixerGroup = aSC.mixerGroup;
        sFX.PlayAudio();
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

    /// <summary>
    /// Creats and array that contais every sound source used.
    /// Serches though the array until it finds the correct file and playes it.
    /// </summary>
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

    /// <summary>
    /// Creates each of the respective element to the array in the unity editor.
    /// </summary>
[Serializable]
public class AudioSoundClip
{
    public string sound_name;
    public AudioClip audio;
    public AudioMixerGroup mixerGroup;
    public bool toggle;
}

[Serializable]

public class VoiceLines
{
    public string voice_name;
    public AudioClip audio;
}