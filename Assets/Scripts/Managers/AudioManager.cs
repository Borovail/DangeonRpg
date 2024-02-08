using Assets.Scripts.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private SoundClip[] soundClips; 

    private Dictionary<SoundType, AudioClip> audioClips = new Dictionary<SoundType, AudioClip>();

    void Start()
    {
        foreach (var clip in soundClips)
        {
            audioClips[clip.soundType] = clip.audioClip;
        }
    }


    public void PlaySound(SoundType soundType,float volume)
    {
        if (audioClips.ContainsKey(soundType))
        {
            AudioSource.PlayClipAtPoint(audioClips[soundType], transform.position,volume);
        }
        else
        {
            Debug.LogError("SoundType " + soundType + " not found in audioClips dictionary");
        }
    }

}

[System.Serializable]
public class SoundClip
{
    public SoundType soundType;
    public AudioClip audioClip;
}


