using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButton : MonoBehaviour
{
    public float delay;
    public BaconSlapper[] stationarySlappers;
    public float thrust;
    public DialogueTrigger dialogueTrigger;
    public string animationStateRef = "isFlying";
    public GameObject blockingBacon;
    public float baconDropSpeed;

    bool isPressed = false;
    bool startAnimating = false;

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
        dialogueTrigger.TriggerDialogue();

        while (blockingBacon.transform.position.y > -13)
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
        startAnimating = true;

        for (int i = 0; i < stationarySlappers.Length; i++)
        {
            stationarySlappers[i].paused = false;
        }

        anim.SetBool(animationStateRef, true);
    }

    private void Update()
    {
        if (!startAnimating)
        { return; }

        transform.Translate(Vector2.up * thrust * Time.deltaTime);

        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }
}
