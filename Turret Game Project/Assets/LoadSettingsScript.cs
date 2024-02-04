using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using System;

public class LoadSettingsScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Master Volume")) PlayerPrefs.SetFloat("Master Volume", 1);
        if (!PlayerPrefs.HasKey("Full Screen")) PlayerPrefs.SetInt("Full Screen", 1);
        if (!PlayerPrefs.HasKey("Screen Resolution")) 
            PlayerPrefs.SetInt("Screen Resolution", 
                Array.FindIndex(Screen.resolutions, element => 
                element.width == Screen.currentResolution.width
                && element.height == Screen.currentResolution.height
                ));
        audioMixer.SetFloat("Master Volume", PlayerPrefs.GetFloat("Master Volume"));
        Screen.fullScreen = PlayerPrefs.GetInt("Full Screen") == 1;
    }
}
