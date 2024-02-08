using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsPanelScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Toggle fullScreenToggle;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private void Awake()
    {
        //Sync settings ui with player prefs
        audioSlider.value = Mathf.Pow(10, PlayerPrefs.GetFloat("Master Volume") / 20);
        fullScreenToggle.isOn = PlayerPrefs.GetInt("Full Screen") == 1;
        #region Sync resolution dropdown ui
        resolutionDropdown.ClearOptions();
        var dropdownOptions = Screen.resolutions.Select(resolution => $"{resolution.width} x {resolution.height}").Distinct().ToList();
        resolutionDropdown.AddOptions(dropdownOptions);

        resolutionDropdown.value = PlayerPrefs.GetInt("Screen Resolution");
        #endregion
    }
    public void OnChangeAudioLevel(float value)
    {
        PlayerPrefs.SetFloat("Master Volume", Mathf.Log10 (value) * 20);
        audioMixer.SetFloat("Master Volume", PlayerPrefs.GetFloat("Master Volume"));
    }
    public void OnChangeFullScreen(bool value)
    {
        PlayerPrefs.SetInt("Full Screen", value ? 1 : 0);
        Screen.fullScreen = value;
    }
    public void OnChangeResolution(int selectedIndex)
    {
        Screen.SetResolution(Screen.resolutions[selectedIndex].width, Screen.resolutions[selectedIndex].height, Screen.fullScreen);
    }
}
