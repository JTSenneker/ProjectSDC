using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class SettingsMenu : MonoBehaviour
{

   

    public AudioMixer audioMixer;
    public Dropdown resolutionsDropdown;
    public Dropdown graphicsDropdown;
    Resolution[] resolutions;
    public int qualityindex;
    public float volume;
    public int GraphicQuality;
    public int ResolutionWidth;
    public int ResolutionHeight;
    public SettingsData settings;

    void Start()
    {
        
       

        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
                resolutionsDropdown.value = currentResolutionIndex;
                resolutionsDropdown.RefreshShownValue();
            }
        }

        resolutionsDropdown.AddOptions(options);

        LoadSettings();
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

   
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SaveSettings()
    {
        Settingsmenusystem.SaveSettings(this);
    }

    public void LoadSettings()
    {
        SettingsData data = Settingsmenusystem.LoadSettings();

        ResolutionWidth = data.ResolultionWidth;
        ResolutionHeight = data.ResolutionHeight;
        volume = data.volume;
        qualityindex = data.GraphicsQuality;

    }
}








