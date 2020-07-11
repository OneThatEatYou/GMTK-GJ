using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacon : MonoBehaviour
{
    public int clogClicks;
    public int maxClogClicks = 10;
    public int freshGB = 90;
    public int unfreshGB = 190;

    protected Rigidbody2D rb;
    protected SpriteRenderer baconRdr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baconRdr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        float freshnessRange = unfreshGB - freshGB;
        float curFreshness = unfreshGB - (freshnessRange / maxClogClicks) * clogClicks;
        baconRdr.color = new Color(1, curFreshness / 255, curFreshness / 255);
    }

    public virtual void OnBaconCollide(Collider2D col)
    {
        col.GetComponent<PlayerController>().Clog(clogClicks, baconRdr.color);
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
