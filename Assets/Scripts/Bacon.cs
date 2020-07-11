using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacon : MonoBehaviour
{
    public int clogClicks;

    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void OnBaconCollide(Collider2D col)
    {
        col.GetComponent<PlayerController>().Clog(clogClicks);
        col.GetComponent<Rigidbody2D>().velocity *= 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnBaconCollide(collision);
    }
}
