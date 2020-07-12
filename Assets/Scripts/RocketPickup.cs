using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpRocket(collision);
    }

    void PickUpRocket(Collider2D col)
    {
        col.GetComponent<PlayerController>().EnableRocket();
        Destroy(gameObject);
    }
}
