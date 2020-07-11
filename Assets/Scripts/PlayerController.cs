using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI clogText;
    public string flyingAnimation = "isFlying";

    [Header("Engine Settings")]
    public float forceMagnitude;
    public float maxUpwardsVelocity;
    public float terminalVelocity;
    public float rotationSpeed;

    Rigidbody2D rb;
    SpriteRenderer rdr;
    Animator anim;
    bool isClogged;
    Color startColor;

    bool IsClogged
    {
        get { return isClogged; }
        set
        {
            isClogged = value;
            clogText.enabled = isClogged;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rdr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

            if (isClogged)
            {
                anim.SetBool(flyingAnimation, false);
            }
            else
            {
                anim.SetBool(flyingAnimation, true);
            }
        }
        else
        {
            anim.SetBool(flyingAnimation, false);
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

        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        float curAngle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.deltaTime);

        //Debug.Log("targetAngle: " + targetAngle);
        //Debug.Log("CurAngle: " + curAngle);

        rb.SetRotation(curAngle);

        rb.AddForce(dir * forceMagnitude, ForceMode2D.Force);

        anim.SetBool(flyingAnimation, true);
    }

    public void Clog(int clicks)
    {
        //wont get clogged again when already clogged
        if (IsClogged)
        { return; }

        IsClogged = true;

        rdr.color = GameManager.Instance.CalculateFreshness(clicks);
        StartCoroutine(Unclog(clicks));

    }

    void UpdateClogText(int remainingClicks)
    {
        Color cloggedColor = GameManager.Instance.CalculateFreshness(remainingClicks);

        clogText.text = remainingClicks.ToString();
        clogText.color = cloggedColor;

        rdr.color = cloggedColor;
    }

    IEnumerator Unclog(int clicks)
    {
        int remainingClicks = clicks;
        UpdateClogText(remainingClicks);

        while (remainingClicks != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                remainingClicks--;
                UpdateClogText(remainingClicks);

                //play particle
            }

            yield return null;
        }
        IsClogged = false;
    }
}
