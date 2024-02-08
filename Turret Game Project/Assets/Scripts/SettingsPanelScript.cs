using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System;
using System.Reflection;

public class SettingsPanelScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Toggle fullScreenToggle;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    [SerializeField] private ResolutionData resolutionData;

    private void Awake()
    {
        //Sync settings ui with player prefs
        audioSlider.value = Mathf.Pow(10, PlayerPrefs.GetFloat("Master Volume") / 20);
        fullScreenToggle.isOn = resolutionData.fullScreen;
        #region Sync resolution dropdown ui
        resolutionDropdown.ClearOptions();
        var dropdownOptions = Screen.resolutions.Select(resolution => $"{resolution.width} x {resolution.height}").Distinct().ToList();
        resolutionDropdown.AddOptions(resolutionData.resolutions.Select(element => new TMP_Dropdown.OptionData(element.width + "x" + element.height)).ToList());

        resolutionDropdown.value = resolutionData.resolutionIndex;
        #endregion
    }
    public void OnChangeAudioLevel(float value)
    {
        PlayerPrefs.SetFloat("Master Volume", Mathf.Log10 (value) * 20);
        audioMixer.SetFloat("Master Volume", PlayerPrefs.GetFloat("Master Volume"));
    }
    public void OnChangeFullScreen(bool value)
    {
        resolutionData.fullScreen =value;
        Screen.fullScreen = resolutionData.fullScreen;
    }
    public void OnChangeResolution(int selectedIndex)
    {
        resolutionData.resolutionIndex = selectedIndex;
        Screen.SetResolution(
            resolutionData.resolutions[resolutionData.resolutionIndex].width,
            resolutionData.resolutions[resolutionData.resolutionIndex].height,
            Screen.fullScreen
            );
    }
}
