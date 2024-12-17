using UnityEngine;

public class AmmunitionPickup : MonoBehaviour
{
    private bool collected;

    public AudioSource PickUpCollectable;

    void Start()
    {
        if (PickUpCollectable != null) PickUpCollectable.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            PlayerMove.instance.activeGun.GetAmmunition();

            Destroy(gameObject);

            PickUpCollectableSound();

            collected = true;
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
