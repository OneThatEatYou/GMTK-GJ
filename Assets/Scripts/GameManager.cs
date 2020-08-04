using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GameManager>();

                if (!instance)
                {
                    Debug.LogWarning("Instance of GameManager not found.");
                }
            }
            return instance;
        }
    }

    #endregion

    public int maxClogClicks = 10;
    public int freshGB = 90;
    public int unfreshGB = 255;

    public Vector2 ScreenWorldSize
    {
        get
        {
            float height = 2f * Camera.main.orthographicSize;
            float width = height * Camera.main.aspect;

            return new Vector2(width, height);
        }
    }

    public Color CalculateFreshness(int clicks)
    {
        float freshnessRange = unfreshGB - freshGB;
        float curFreshness = unfreshGB - (freshnessRange / maxClogClicks) * clicks;

        return new Color(1, curFreshness / 255, curFreshness / 255);
    }
}
