using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaconBullet : Bacon
{
    public float speed;

    void FixedUpdate()
    {
        if (transform.position.x < Camera.main.transform.position.x + GameManager.Instance.ScreenWorldSize.x && transform.position.x > Camera.main.transform.position.x - GameManager.Instance.ScreenWorldSize.x)
        {
            rb.MovePosition(rb.position + (Vector2)transform.up * speed);
        }
        else
        { gameObject.SetActive(false); }
    }
}
