using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(instance);
    }
    public void PlaySound(AudioClip _audio)
    {
        source.PlayOneShot(_audio,0.6f);
        source.volume = 0.5f;
    }
    public void StopSound()
    {
        source.Stop();
    }
}
