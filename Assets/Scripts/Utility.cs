using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public const string filePath = "Assets/Screenshots/";
    public int startingInt;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string name = filePath + "index_" + startingInt.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(name);
            startingInt++;

            Debug.Log("Took a screenshot");
        }
    }
}
