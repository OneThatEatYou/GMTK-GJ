using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacon : MonoBehaviour
{
    public int clogClicks;

    public bool paused = false;

    protected Rigidbody2D rb;
    protected SpriteRenderer baconRdr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baconRdr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        baconRdr.color = GameManager.Instance.CalculateFreshness(clogClicks);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnBaconCollide(collision.collider);
    }
}
