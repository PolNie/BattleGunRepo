using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int maxHealth, currentHealth;
    public float timeUntilFinalScreen = 1f;
    public string finalScreenScene;

    public AudioSource playerDieSound;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerDieSound != null) playerDieSound.enabled = false;

        int difficulty = PlayerPrefs.GetInt("Difficulty", 0);
        AdjustPlayerHealth(difficulty);

        currentHealth = maxHealth;
        UI.instance.healthSlider.maxValue = maxHealth;
        UI.instance.healthSlider.value = currentHealth;
        UI.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;

        UI.instance.ShowDamage();

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            PlayerDieSound();

            GameManager.instance.PlayerDied();
        }

        UI.instance.healthSlider.value = currentHealth;
        UI.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }

    public void HealPlayer(int heal)
    {
        currentHealth += heal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UI.instance.healthSlider.value = currentHealth;
        UI.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }

    public IEnumerator WaitingForFinalScreen()
    {
        yield return new WaitForSeconds(timeUntilFinalScreen);

        SceneManager.LoadScene(finalScreenScene);

        Cursor.lockState = CursorLockMode.None;
    }

    void AdjustPlayerHealth(int difficulty)
    {
        switch (difficulty)
        {
            case 0: // Easy
                maxHealth = 150;
                break;
            case 1: // Normal
                maxHealth = 100;
                break;
            case 2: // Hard
                maxHealth = 75;
                break;
        }
    }

    private void PlayerDieSound()
    {
        if (playerDieSound != null)
        {
            playerDieSound.enabled = true;
            playerDieSound.Play();
        }
    }
}
