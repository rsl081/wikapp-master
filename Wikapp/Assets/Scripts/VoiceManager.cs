using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip[] voiceClips;
    int index = 0;
    [SerializeField] bool isAutoPlay;
    [SerializeField] float startVol = 2f;
    [SerializeField] float volume = 0.7f;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
        //voiceClips = GetComponents<AudioClip>();
        if(isAutoPlay)
        {
            source.PlayOneShot(voiceClips[index], startVol);
        }
    }

    public void PlayVoice()
    {
        source.PlayOneShot(voiceClips[index], volume);
    }
    public void PlayVoiceTutorial(int index)
    {
        source.PlayOneShot(voiceClips[index], volume);
    }

    public void StopVoice()
    {
        source.Stop();
    }

    public void NextVoice()
    {
        
        if(index < voiceClips.Length)
        {
            Debug.Log(index < voiceClips.Length);
            index++;
            source.PlayOneShot(voiceClips[index], volume);

        }
    }

    public void PrevVoice()
    {
        if(0 < index)
        {
            index--;
            source.PlayOneShot(voiceClips[index], volume);
        }
    }

}
