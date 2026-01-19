using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Singelton<AudioSystem>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private AudioClip hoverCard;

    void Start()
    {
        PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayHitSFX()
    {
        if (hitSFX == null || sfxSource == null) return;
        sfxSource.PlayOneShot(hitSFX);
    }

    public void PlayHoverSFX()
    {
        if (hitSFX == null || sfxSource == null) return;
        sfxSource.PlayOneShot(hoverCard);
    }
}