using UnityEngine;
using TMPro;
using System.Collections;

public class DoorController : MonoBehaviour
{
    private bool isPlayerNear = false;
    private bool canOpen = false;
    public int requiredEnemiesToKill = 5;

    public TextMeshProUGUI promptText;
    private bool hasBeenOpened = false;

    public AudioSource openDoorSound;

    // Start is called before the first frame update
    void Start()
    {
        if (openDoorSound != null) openDoorSound.enabled = false;

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        canOpen = GameManager.instance.killedEnemies >= requiredEnemiesToKill;

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !hasBeenOpened)
        {
            if (canOpen)
            {
                OpenDoor();
            }
            else
            {
                if (promptText != null)
                {
                    promptText.text = "You need to kill " + (requiredEnemiesToKill - GameManager.instance.killedEnemies) + " enemies to open the door.";
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }

    void OpenDoor()
    {
        if (!hasBeenOpened)
        {
            hasBeenOpened = true;

            gameObject.SetActive(false);

            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }

            OpenDoorSound();
        }
    }

    private void OpenDoorSound()
    {
        if (openDoorSound != null)
        {
            openDoorSound.enabled = true;
            openDoorSound.Play();
        }
    }
}
