using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;

    [Header("Resolution")]
    [SerializeField] TMP_Dropdown resDropdown;
    [SerializeField] Toggle fullscreenToggle;
    Resolution[] resolutions;

    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        GetResolutionOptions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", ConvertToDec(masterVolumeSlider.value));
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", ConvertToDec(musicVolumeSlider.value));
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", ConvertToDec(SFXVolumeSlider.value));
        PlayerPrefs.SetFloat("SFXVolume", SFXVolumeSlider.value);
    }

    float ConvertToDec(float sliderValue)
    {
        return Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20;
    }

    void GetResolutionOptions()
    {
        resDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            TMP_Dropdown.OptionData newOption;
            newOption = new TMP_Dropdown.OptionData(resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString());
            resDropdown.options.Add(newOption);
        }
    }

    public void ChooseResolution()
    {
        Screen.SetResolution(resolutions[resDropdown.value].width, resolutions[resDropdown.value].height, fullscreenToggle.isOn);
    }

    public void OpenOptions()
    {
        canvas.enabled = true;
    }

    public void CloseOptions()
    {
        canvas.enabled = false;
    }
}
