using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPickup : MonoBehaviour
{
    public AudioClip sfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpRocket(collision);
    }

    void PickUpRocket(Collider2D col)
    {
        AudioManager.PlayClipAtPoint(sfx, transform.position);

        col.GetComponent<PlayerController>().EnableRocket();
        Destroy(gameObject);
    }
}
