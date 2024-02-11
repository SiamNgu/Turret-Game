using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public int maxAudioSources = 10; // Adjust this based on your game's needs

    private AudioSource[] audioSources;

    private void Awake()
    {
        audioSources = Array.ConvertAll(new AudioSource[maxAudioSources], _ => Instantiate(new GameObject("Sound", typeof(AudioSource)), transform).GetComponent<AudioSource>());
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource freeSource = GetFreeAudioSource();
        if (freeSource != null)
        {
            freeSource.clip = clip;
            freeSource.Play();
        }
        else
        {
            Debug.LogWarning("No free AudioSources available to play sound: " + clip.name);
        }
    }

    private AudioSource GetFreeAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }
}
