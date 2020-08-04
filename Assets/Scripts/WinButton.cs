using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinButton : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public AudioClip soundEffect;
    public float vol = 0.8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameOver();
    }

    void GameOver()
    {
        gameOverCanvas.SetActive(true);
        FindObjectOfType<PlayerController>().CanMove();

        var s = AudioManager.PlayClipAtPoint(soundEffect, Vector2.zero);
        s.volume = vol;
    }
}
