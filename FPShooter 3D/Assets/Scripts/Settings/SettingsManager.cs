using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown difficultyDropdown;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown displayDropdown;
    public Toggle musicToggle;
    public Toggle soundsToggle;
    public AudioMixer audioMixer;

    private int previousDifficulty;
    private int previousResolution;
    private int previousDisplayMode;
    private bool previousMusicSetting;
    private bool previousSoundsSetting;

    public AudioSource buttonClickAudio;

    private string previousScene = "MainMenu";

    private Resolution[] commonResolutions = new Resolution[]
    {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 2560, height = 1440 },
        new Resolution { width = 3840, height = 2160 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1024, height = 768 },
        new Resolution { width = 1440, height = 900 }
    };

    private string[] displayModes = new string[]
    {
        "Full Screen",
        "Borderless window",
        "Window"
    };

    void Start()
    {
        if (buttonClickAudio != null) buttonClickAudio.enabled = false;

        LoadSettings();

        previousDifficulty = difficultyDropdown.value;
        previousResolution = resolutionDropdown.value;
        previousDisplayMode = displayDropdown.value;
        previousMusicSetting = musicToggle.isOn;
        previousSoundsSetting = soundsToggle.isOn;

        PopulateResolutionDropdown();
        PopulateDisplayDropdown();

        SetAudioSettings();
    }

    void Update()
    {

    }

    public void ApplyAndExit()
    {
        PlayButtonSound();

        PlayerPrefs.SetInt("Difficulty", difficultyDropdown.value);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("DisplayMode", displayDropdown.value);
        PlayerPrefs.SetInt("Music", musicToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Sounds", soundsToggle.isOn ? 1 : 0);

        PlayerPrefs.Save();

        ApplyResolutionAndDisplay();

        SetAudioSettings();

        SceneManager.LoadScene(previousScene);
    }

    public void Cancel()
    {
        PlayButtonSound();

        LoadSettings();

        SceneManager.LoadScene(previousScene);
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficultyDropdown.value = PlayerPrefs.GetInt("Difficulty");
        }

        if (PlayerPrefs.HasKey("Resolution"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        }

        if (PlayerPrefs.HasKey("DisplayMode"))
        {
            displayDropdown.value = PlayerPrefs.GetInt("DisplayMode");
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            musicToggle.isOn = PlayerPrefs.GetInt("Music") == 1;
        }

        if (PlayerPrefs.HasKey("Sounds"))
        {
            soundsToggle.isOn = PlayerPrefs.GetInt("Sounds") == 1;
        }
    }

    private void PopulateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        var options = new List<string>();
        foreach (var resolution in commonResolutions)
        {
            options.Add(resolution.width + "x" + resolution.height);
        }
        resolutionDropdown.AddOptions(options);

        int savedResolution = PlayerPrefs.GetInt("Resolution", 0);
        resolutionDropdown.value = Mathf.Clamp(savedResolution, 0, commonResolutions.Length - 1);
    }

    private void PopulateDisplayDropdown()
    {
        displayDropdown.ClearOptions();
        displayDropdown.AddOptions(new List<string>(displayModes));

        int savedDisplayMode = PlayerPrefs.GetInt("DisplayMode", 0);
        displayDropdown.value = Mathf.Clamp(savedDisplayMode, 0, displayModes.Length - 1);
    }

    private void ApplyResolutionAndDisplay()
    {
        var selectedResolution = commonResolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        switch (displayDropdown.value)
        {
            case 0: // Full Screen
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1: // Borderless window
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2: // Window
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    private void SetAudioSettings()
    {
        if (audioMixer != null)
        {
            if (musicToggle.isOn)
            {
                audioMixer.SetFloat("MusicVolume", -26); // Normal Volume
            }
            else
            {
                audioMixer.SetFloat("MusicVolume", -80); // Muted Volume
            }

            if (soundsToggle.isOn)
            {
                audioMixer.SetFloat("SoundVolume", -6); // Normal Volume
            }
            else
            {
                audioMixer.SetFloat("SoundVolume", -80); // Muted Volume
            }
        }
    }

    private void PlayButtonSound()
    {
        if (buttonClickAudio != null)
        {
            buttonClickAudio.enabled = true;
            buttonClickAudio.Play();
        }
    }
}