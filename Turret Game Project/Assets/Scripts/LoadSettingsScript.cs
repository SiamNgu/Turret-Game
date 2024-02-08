using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Linq;

public class LoadSettingsScript : MonoBehaviour
{
    [SerializeField] private ResolutionData resolutionData; 
    [SerializeField] private AudioMixer audioMixer;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Master Volume")) PlayerPrefs.SetFloat("Master Volume", 1);
        if (resolutionData.resolutions.Length <= 0)
            resolutionData.resolutions = Screen.resolutions.Select(resolution => new ResolutionData.MyResolution { width = resolution.width, height = resolution.height }).ToArray();

        audioMixer.SetFloat("Master Volume", PlayerPrefs.GetFloat("Master Volume"));
        Screen.fullScreen = resolutionData.fullScreen;
    }
}
