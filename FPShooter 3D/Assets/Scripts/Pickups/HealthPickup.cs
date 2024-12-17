using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int heal;

    public AudioSource PickUpCollectable;

    void Start()
    {
        if (PickUpCollectable != null) PickUpCollectable.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerHealth.instance.currentHealth < PlayerHealth.instance.maxHealth)
            {
                PlayerHealth.instance.HealPlayer(heal);
                Destroy(gameObject);
                PickUpCollectableSound();
            }
            else
            {
                Debug.Log("Health Pack not collected: life at maximum.");
            }
        }
    }

    private void PickUpCollectableSound()
    {
        if (PickUpCollectable != null)
        {
            PickUpCollectable.enabled = true;
            PickUpCollectable.Play();
        }
    }
}
