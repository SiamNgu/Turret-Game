using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Data
{
    // Resolution
    public static int SetResolutionIndex(ResolutionData resolutionData)
    {
        if (!PlayerPrefs.HasKey("Resolution Index"))
            PlayerPrefs.SetInt("Resolution Index", Array.FindIndex(resolutionData.resolutions, element => 
            element.width == Screen.currentResolution.width && 
            element.height == Screen.currentResolution.height
            ));
        return PlayerPrefs.GetInt("Resolution Index");
    }
    public static void SetResolutionIndex(int index)
    {
        PlayerPrefs.SetInt("Resolution Index", index);
    }
    //Full Screen
    public static void SetFullScreen(bool isFullScreen)
    {
        PlayerPrefs.SetInt("Full Screen", isFullScreen ? 1 : 0);
    }
    public static bool IsFullScreen()
    {
        if (!PlayerPrefs.HasKey("Full Screen"))
            PlayerPrefs.SetInt("Full Screen", 1);
        return PlayerPrefs.GetInt("Full Screen") == 1;
    }
    //Sound
    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("Master Volume", volume);
    }
    public static float GetMasterVolume()
    {
        if (!PlayerPrefs.HasKey("Master Volume"))
            PlayerPrefs.SetFloat("Master Volume", 1);
        return PlayerPrefs.GetFloat("Master Volume");
    }
}
