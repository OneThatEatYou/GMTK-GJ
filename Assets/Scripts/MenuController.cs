using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MenuController : MonoBehaviour
{
    public TimelineAsset introTimeline;
    public Dialogue dialogue;
    public float fadeTime;

    bool playedAnim = false;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (playedAnim)
        { return; }

        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        GameManager.Instance.PlayCutscene(introTimeline);
        DialogueManager.Instance.Dialogue = dialogue;
        playedAnim = true;
        StartCoroutine(FadeoutTitle(fadeTime));
    }

    IEnumerator FadeoutTitle(float fadeTime)
    {
        float t = 0f;

        while (canvasGroup.alpha != 0)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
