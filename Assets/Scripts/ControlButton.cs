using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButton : MonoBehaviour
{
    [Header("Animation Setttings")]
    //number of seconds to wait after bacon fall for button to fly
    public float delay;
    public float thrust;
    public string animationStateRef = "isFlying";
    public GameObject blockingBacon;
    public float baconDropSpeed;

    [Space]
    public Dialogue dialogue;

    bool isPressed = false;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPressed)
        { return; }

        isPressed = true;

        StartCoroutine(OOCAnimation());
    }

    IEnumerator OOCAnimation()
    {
        DialogueManager.Instance.Dialogue = dialogue;

        while (blockingBacon.transform.position.y > Camera.main.transform.position.y - GameManager.Instance.ScreenWorldSize.y)
        {
            blockingBacon.transform.Translate(Vector2.down * baconDropSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(blockingBacon);

        yield return new WaitForSeconds(delay);

        OutOfControl();
    }

    void OutOfControl()
    {
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
        while (transform.position.y < Camera.main.transform.position.y + GameManager.Instance.ScreenWorldSize.y)
        {
            transform.Translate(Vector2.up * thrust * Time.deltaTime);

            yield return null;
        }

        Destroy(gameObject);
    }
}
