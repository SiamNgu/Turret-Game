using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class SettingsPanelScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;

    #if !UNITY_IOS && !UNITY_ANDROID

    [SerializeField] private Toggle fullScreenToggle;

    [SerializeField] private TMP_Dropdown resolutionDropdown;


    [SerializeField] private ResolutionData resolutionData;
    #endif
    private void Awake()
    {
        //Sync settings ui with player prefs
        audioSlider.value = Mathf.Pow(10, Data.GetMasterVolume() / 20);

        #if !UNITY_ANDROID && !UNITY_IOS
            fullScreenToggle.isOn = Data.IsFullScreen();

            resolutionDropdown.ClearOptions();
            var dropdownOptions = resolutionData.resolutions.Select(resolution => $"{resolution.width} x {resolution.height}").Distinct().ToList();
            resolutionDropdown.AddOptions(dropdownOptions);
            resolutionDropdown.value = Data.SetResolutionIndex(resolutionData);
        #endif
    }
    public void OnChangeAudioLevel(float value)
    {
        Data.SetMasterVolume( Mathf.Log10 (value) * 20);
        audioMixer.SetFloat("Master Volume", Data.GetMasterVolume());
    }

    #if !UNITY_ANDROID && !UNITY_IOS

        public void OnChangeFullScreen(bool value)
        {
            Data.SetFullScreen( value );
            Screen.fullScreen = Data.IsFullScreen();
        }
        public void OnChangeResolution(int selectedIndex)
        {
            Data.SetResolutionIndex(selectedIndex);
            Screen.SetResolution(
                resolutionData.resolutions[PlayerPrefs.GetInt("Resolution Index")].width,
                resolutionData.resolutions[PlayerPrefs.GetInt("Resolution Index")].height,
                Screen.fullScreen
                );
        }
    #endif
}
