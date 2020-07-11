using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacannon : MonoBehaviour
{
    public GameObject bulletPerfab;
    public Transform shootPos;
    public float reloadTime;
    public float angle;
    public float destroyDelay;

    private void Start()
    {
        StartCoroutine(LaunchBacon(reloadTime));
    }

    IEnumerator LaunchBacon(float reloadTime)
    {
        GameObject obj = Instantiate(bulletPerfab, shootPos.position, Quaternion.Euler(0, 0, angle));
        Destroy(obj, destroyDelay);

        yield return new WaitForSeconds(reloadTime);

        StartCoroutine(LaunchBacon(reloadTime));
    }
}
