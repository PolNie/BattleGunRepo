using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public string mainMenu, settingsScenne;

    public GameObject confirmationPanel;
    public GameObject resumeButton, mainMenuButton, settingsButton, quitButton;

    public AudioSource buttonClickSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (buttonClickSound != null) buttonClickSound.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
        PlayButtonClickSound();
    }

    public void MainMenu()
    {
        GameManager.instance.ResetTimeScale();
        SceneManager.LoadScene(mainMenu);
        PlayButtonClickSound();
    }

    public void Quit()
    {
        Application.Quit();
        PlayButtonClickSound();
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.enabled = true;
            buttonClickSound.Play();
        }
    }
}
