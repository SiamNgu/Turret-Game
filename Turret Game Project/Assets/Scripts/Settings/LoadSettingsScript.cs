using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using TMPro;

public class LoadSettingsScript : MonoBehaviour
{
    [SerializeField] private ResolutionData resolutionData; 
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        //Set default settings when game starts for the first time
        if (resolutionData.resolutions.Length == 0)
            resolutionData.resolutions = Screen.resolutions.Select(resolution => new ResolutionData.MyResolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        audioMixer.SetFloat("Master Volume", Data.GetMasterVolume());
        Screen.SetResolution(resolutionData.resolutions[Data.SetResolutionIndex(resolutionData)].width, resolutionData.resolutions[Data.SetResolutionIndex(resolutionData)].height, Data.IsFullScreen());
    }
}
