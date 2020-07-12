using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealControlButton : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameOver();
    }

    void GameOver()
    {
        gameOverCanvas.SetActive(true);
        FindObjectOfType<PlayerController>().CanMove();
    }
}
