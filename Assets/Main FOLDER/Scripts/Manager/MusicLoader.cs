using UnityEngine;
using Random = UnityEngine.Random;

public class MusicLoader : MonoBehaviour
{
    private AudioSource audioSource;
    private short rndClip;
    
    public AudioClip[] musicClips;

    public bool onlyMain;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Start()
    {
        if (!onlyMain)
        {
            rndClip = (short)Random.Range(0, musicClips.Length);
            audioSource.clip = musicClips[rndClip];
        }
        else
        {
            audioSource.clip = musicClips[0];
        }
        
        audioSource.Play();
    }
}
