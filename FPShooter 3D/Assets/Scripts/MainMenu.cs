using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public string firstLevel, settings;
    public string thisScene;

    public AudioSource buttonClickAudio;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (buttonClickAudio != null) buttonClickAudio.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        PlayButtonSound();
        SceneManager.LoadScene(firstLevel);
    }

    public void GameSettings()
    {
        PlayButtonSound();
        SceneManager.LoadScene(settings);
    }

    public void QuitGame()
    {
        PlayButtonSound();
        Application.Quit();
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
