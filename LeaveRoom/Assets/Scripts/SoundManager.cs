using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I;

    [SerializeField] float maxVolume = 1.0f;
    [SerializeField] AudioSource bgmSource;
    [SerializeField] List<AudioSource> seSource;
    [SerializeField] float volumeSeemSecond = 0.5f;

    public enum Type
    {
        Home,
        Room,
        OpenDoor,
        Driver,
        CannotOpenHikidasi,
        OpenHikidasi,
        OpenDeskKey,
    }
    
    [Serializable]
    public class AudioClipContext
    {
        public Type BgmType;
        public AudioClip AudioClip;
    }

    [SerializeField] List<AudioClipContext> audioClipContexts;

    AudioClip nextPlayAudioClip;
    
    void Awake()
    {
        if (I)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        I = this;
    }

    public void PlayBgm(Type type)
    {
        StopAllCoroutines();

        var audioClip = GetAudioClip(type);
        nextPlayAudioClip = audioClip;

        StartCoroutine(SeemBgm());
    }

    int seIndex;
    public void PlaySe(Type type)
    {
        var audioClip = GetAudioClip(type);

        seSource[seIndex].clip = audioClip;
        seSource[seIndex].Play();
        
        seIndex++;
        if (seIndex >= seSource.Count)
        {
            seIndex = 0;
        }
    }

    AudioClip GetAudioClip(Type type)
    {
        var context = audioClipContexts.First(c => c.BgmType == type);
        return context.AudioClip;
    }
    
    bool seemingNow = false;
    IEnumerator SeemBgm()
    {
        seemingNow = true;
        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= (maxVolume / 60) * volumeSeemSecond;
            yield return null;
        }
        
        bgmSource.Stop();
        yield return null;
        
        bgmSource.volume = 0f;
        bgmSource.clip = nextPlayAudioClip;
        bgmSource.time = 0;
        bgmSource.loop = true;
        bgmSource.Play();
        
        while (bgmSource.volume < maxVolume)
        {
            bgmSource.volume += (maxVolume / 60) * volumeSeemSecond;
            yield return null;
        }
        
        bgmSource.volume = maxVolume;
        nextPlayAudioClip = null;

        seemingNow = false;
    }
}