using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;

    float[] parallaxScales;

    public float smoothing = 1f;

    Transform cam;
    Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;

        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //scrolls opposite direction to camera movement
            float parallaxY = (previousCamPos.y - cam.position.y) * parallaxScales[i];

            float backgroundTargetPosY = backgrounds[i].position.y + parallaxY;

            Vector3 backgroundTargetPos = new Vector3(backgrounds[i].transform.position.x, backgroundTargetPosY, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }
}
