using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip playButtonClip;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
