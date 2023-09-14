using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip carDamage;
    public AudioClip carJump;
    public AudioClip carLand;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    //singleton implementation
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = new SoundManager();
            
            return instance;
        }
    }

    public void PlayOneShot(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
