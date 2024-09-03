using ProjectTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [SerializeField] SerializableDictionary<string, AudioClip> soundDict;
    [SerializeField] AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(string id)
    {
        audioSource.clip = soundDict[id];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlaySFX(string id)
    {
        audioSource.PlayOneShot(soundDict[id]);
    }
}
