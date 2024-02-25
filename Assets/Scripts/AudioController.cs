using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;
    public AudioClip musicClip;
    public AudioClip cherryClip;
    public AudioClip hurtClip;
    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
        
    }

  
}
