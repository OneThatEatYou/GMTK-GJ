using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Engine Settings")]
    public float forceMagnitude;
    public float maxUpwardsVelocity;
    public float terminalVelocity;

    [Header("Feedbacks")]
    public Color cloggedColor;
    Color startColor;

    Rigidbody2D rb;
    SpriteRenderer rdr;
    bool isClogged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rdr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        startColor = rdr.color;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
            Fly();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, terminalVelocity, maxUpwardsVelocity));
    }

    void Fly()
    {
        //cant fly when clogged
        if (isClogged)
        { return; }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - (Vector2)transform.position;
        dir = dir.normalized;

        rb.AddForce(dir * forceMagnitude, ForceMode2D.Force);
    }

    public void Clog(int clicks)
    {
        //wont get clogged again when already clogged
        if (isClogged)
        { return; }

        isClogged = true;

        rdr.color = cloggedColor;
        StartCoroutine(Unclog(clicks));

    }

    IEnumerator Unclog(int clicks)
    {
        int remainingClicks = clicks;

        while (remainingClicks != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                remainingClicks--;
                //play particle
            }

            yield return null;
        }

        rdr.color = startColor;
        isClogged = false;

        
    }
}
