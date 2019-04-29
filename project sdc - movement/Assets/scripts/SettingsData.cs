using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class SettingsData 
{

    public int GraphicsQuality;
    public bool Fullscreen = true;
    public float volume;
    public int ResolultionWidth = 1920;
    public int ResolutionHeight = 1080;

    public SettingsData(SettingsMenu settings)
    {
        GraphicsQuality = settings.GraphicQuality;
        ResolultionWidth = settings.ResolutionWidth;
        ResolutionHeight = settings.ResolutionHeight;
      
        volume = settings.volume;

    }

}
