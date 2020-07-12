using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    public static DialogueManager instance;

    public static DialogueManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<DialogueManager>();

                if (!instance)
                {
                    Debug.LogWarning("Instance of DialogueManager not found.");
                }
            }
            return instance;
        }
    }

    #endregion

    public TextMeshProUGUI dialogueText;
    public float timeBetweenLetter = 0.1f;
    public float timeBeforeEnding = 3f;
    public AudioClip clip;

    Queue<string> sentences = new Queue<string>();

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            PlayAudio();
            yield return new WaitForSeconds(timeBetweenLetter);
        }

        yield return new WaitForSeconds(timeBeforeEnding);
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        dialogueText.text = "";
    }

    void PlayAudio()
    {
        if(!clip)
        { return; }

        source.clip = clip;
        source.Play();
    }
}
