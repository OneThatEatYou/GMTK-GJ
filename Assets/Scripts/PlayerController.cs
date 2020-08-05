using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI clogText;
    public string flyingAnimation = "isFlying";
    public GameObject rocketObj;
    public GameObject particle;
    public AudioClip clogSFX;
    public AudioClip unclogSFX;
    public Vector2 unclogPitchRange;

    [Header("Engine Settings")]
    public float forceMagnitude;
    public float maxUpwardsVelocity;
    public float terminalVelocity;
    public float rotationSpeed;

    [Header("Other Settings")]
    public float walkingSpeed;

    Rigidbody2D rb;
    SpriteRenderer rdr;
    Animator anim;
    AudioSource audioSource;
    bool isClogged;
    Color startColor;
    bool hasRocket = false;
    bool canMove = false;

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
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        startColor = rdr.color;
    }

    void Update()
    {
        if (!canMove)
        { return; }

        if (!hasRocket)
        {
            float horizontal = Input.GetAxis("Horizontal");

            Move(horizontal);

            return;
        }

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

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
        else
        {
            anim.SetBool(flyingAnimation, false);
            audioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, terminalVelocity, maxUpwardsVelocity));
    }

    public void CanMove()
    {
        canMove = !canMove;
    }

    public void EnableRocket()
    {
        rocketObj.SetActive(true);
        hasRocket = true;
    }

    void Move(float horizontalInput)
    {
        rb.velocity = new Vector2(horizontalInput * walkingSpeed, rb.velocity.y);
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

        InstantiateParticle(clicks);

        AudioManager.PlayClipAtPoint(clogSFX, transform.position);
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

                InstantiateParticle(remainingClicks);

                var s = AudioManager.PlayClipAtPoint(unclogSFX, transform.position);
                s.pitch = Random.Range(unclogPitchRange.x, unclogPitchRange.y);
            }

            yield return null;
        }
        IsClogged = false;
    }

    void InstantiateParticle(int clicks)
    {
        Color col = GameManager.Instance.CalculateFreshness(clicks);
        GameObject obj = Instantiate(particle, transform.position, Quaternion.identity);
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = col;

        Destroy(obj, main.startLifetime.constantMax);
    }
}
