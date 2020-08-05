using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ControlButton : MonoBehaviour
{
    [Header("Animation Setttings")]
    public TimelineAsset oocTimeline;
    public float maxThrust;
    public float accelTime;
    public string animationStateRef = "isFlying";
    public Sprite pressedButton;
    public AudioClip sfx;

    [Space]
    public Dialogue dialogue;

    bool isPressed = false;
    Animator anim;
    SpriteRenderer rend;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPressed)
        { return; }

        isPressed = true;

        GameManager.Instance.PlayCutscene(oocTimeline);
        DialogueManager.Instance.Dialogue = dialogue;
        rend.sprite = pressedButton;
        AudioManager.PlayClipAtPoint(sfx, Vector2.zero);

        GameManager.Instance.startTime = Time.time;
    }

    public void OutOfControl()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(FlyButton());

        Bacon[] bacons = FindObjectsOfType<Bacon>();

        for (int i = 0; i < bacons.Length; i++)
        {
            bacons[i].paused = false;
        }

        anim.SetBool(animationStateRef, true);
    }

    IEnumerator FlyButton()
    {
        float time = 0f;

        while (transform.position.y < Camera.main.transform.position.y + GameManager.Instance.ScreenWorldSize.y)
        {
            float thrust = maxThrust * Mathf.Sin((time / accelTime) * (Mathf.PI / 2));
            transform.Translate(Vector2.up * thrust * Time.deltaTime);

            if (time < accelTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = accelTime;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
