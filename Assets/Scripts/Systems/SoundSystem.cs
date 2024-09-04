using ProjectTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [SerializeField] SerializableDictionary<string, AudioClip> soundDict;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource bgmSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        UpdateVolume();
    }

    public void PlayBGM(string id)
    {
        bgmSource.clip = soundDict[id];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(string id)
    {
        sfxSource.PlayOneShot(soundDict[id]);
    }

    public void UpdateVolume()
    {
        sfxSource.volume = PlayerPrefs.GetFloat("SFX", 0.5f);
        bgmSource.volume = PlayerPrefs.GetFloat("BGM", 0.5f);
    }
}
