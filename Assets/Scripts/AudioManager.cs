using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioSource PlayClipAtPoint(AudioClip clip, Vector2 pos)
    {
        GameObject obj = new GameObject("TempAudio");
        obj.transform.position = pos;
        AudioSource aSource = obj.AddComponent<AudioSource>();
        aSource.spatialBlend = 0;
        aSource.clip = clip;
        aSource.Play();

        Destroy(obj, clip.length);

        return aSource;
    }
}
