using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaconBullet : Bacon
{
    public float speed;

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2)transform.up * speed);
    }
}
