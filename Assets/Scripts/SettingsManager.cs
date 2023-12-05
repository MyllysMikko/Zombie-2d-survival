using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropDown;
    [SerializeField] Toggle fullscreenToggle;

    int resolutionIndex;
    bool fullscreen;

    Vector2[] resolutions =
    {
        new Vector2(1920, 1090),
        new Vector2(1600, 900),
        new Vector2(1280, 720)
    };


    void Start()
    {
        LoadPrefs();
        fullscreenToggle.isOn = fullscreen;

        dropDown.ClearOptions();
        List<string> options = new List<string>();
        foreach (Vector2 resolution in resolutions)
        {
            options.Add($"{resolution.x} x {resolution.y}");
        }
        dropDown.AddOptions(options);
        dropDown.value = resolutionIndex;
        dropDown.RefreshShownValue();

        SetResolution();
    }

    public void SetResolution()
    {
        Vector2 resolution = resolutions[resolutionIndex];
        Screen.SetResolution((int)resolution.x, (int)resolution.y, fullscreen);

    }


    public void OnResolutionChange(int index)
    {
        resolutionIndex = index;
        SetResolution();
        SavePrefs();
    }

    public void OnFullscreenChange(bool change)
    {
        if (change != fullscreen)
        {
            fullscreen = change;
            SetResolution();
            SavePrefs();
        }
    }

    void LoadPrefs()
    {
        resolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0);
        fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
    }

    void SavePrefs()
    {
        PlayerPrefs.SetInt("fullscreen", Convert.ToInt32(fullscreen));
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }
}