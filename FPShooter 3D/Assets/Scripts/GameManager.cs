using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float waitAfterDeath = 3f;
    public int killedEnemies;
    public int requiredEnemiesToWin = 10;
    public bool playerWon = false;

    public AudioSource musicAudioSource;
    public AudioSource soundsAudioSource;
    public float loweredVolume = 0.2f;
    public float normalVolume = 1f;
    public float muteVolume = 0f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (UI.instance.pauseScreen.activeInHierarchy)
        {
            UI.instance.pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;

            if (musicAudioSource != null)
            {
                musicAudioSource.volume = normalVolume;
                soundsAudioSource.volume = normalVolume;
            }
        }
        else
        {
            UI.instance.pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;

            if (musicAudioSource != null)
            {
                musicAudioSource.volume = loweredVolume;
                soundsAudioSource.volume = muteVolume;
            }
        }
    }

    public void CheckWinCondition()
    {
        if (killedEnemies >= requiredEnemiesToWin)
        {
            playerWon = true;
            LoadFinalScreen();
        }
    }

    public void PlayerDied()
    {
        playerWon = false;
        LoadFinalScreen();
    }

    private void LoadFinalScreen()
    {
        SceneManager.LoadScene("FinalScreen");
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }

}
