using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreen : MonoBehaviour
{
    public static FinalScreen instance;

    public string mainMenuScene, playAgain;

    public TextMeshProUGUI killedEnemiesText, youWin, youLoose;
    public GameObject winImage, loseImage;

    public AudioSource winAudioSource;
    public AudioSource loseAudioSource;
    public AudioSource buttonClickAudio;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (winAudioSource != null) winAudioSource.enabled = false;
        if (loseAudioSource != null) loseAudioSource.enabled = false;
        if (buttonClickAudio != null) buttonClickAudio.enabled = false;

        killedEnemiesText.text = "Killed Enemies: " + GameManager.instance.killedEnemies;

        if (GameManager.instance.playerWon)
        {
            winImage.SetActive(true);
            loseImage.SetActive(false);
            youWin.gameObject.SetActive(true);
            youLoose.gameObject.SetActive(false);
            PlayAudio(winAudioSource);
        }
        else
        {
            winImage.SetActive(false);
            loseImage.SetActive(true);
            youWin.gameObject.SetActive(false);
            youLoose.gameObject.SetActive(true);
            PlayAudio(loseAudioSource);
        }
    }

    private void PlayAudio(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.enabled = true;
            audioSource.Play();
        }
    }

    public void PlayAgain()
    {
        PlayButtonSound();
        SceneManager.LoadScene(playAgain);
    }

    public void MainMenu()
    {
        PlayButtonSound();
        SceneManager.LoadScene(mainMenuScene);
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
