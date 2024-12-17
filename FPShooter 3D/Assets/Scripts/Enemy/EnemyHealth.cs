using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty", 0);
        AdjustEnemyHealth(difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageEnemy(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.killedEnemies++;
            GameManager.instance.CheckWinCondition();
            UI.instance.killedEnemiesText.text = "Killed Enemies: " + GameManager.instance.killedEnemies;
        }
    }

    void AdjustEnemyHealth(int difficulty)
    {
        switch (difficulty)
        {
            case 0: // Easy
                currentHealth = 15;
                break;
            case 1: // Normal
                currentHealth = 30;
                break;
            case 2: // Hard
                currentHealth = 70;
                break;
        }
    }
}
