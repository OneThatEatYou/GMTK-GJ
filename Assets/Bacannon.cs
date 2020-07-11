using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacannon : MonoBehaviour
{
    public GameObject bulletPerfab;
    public Transform shootPos;
    public float reloadTime;
    public float angle;

    private void Start()
    {
        StartCoroutine(LaunchBacon(reloadTime));   
    }

    IEnumerator LaunchBacon(float reloadTime)
    {
        Instantiate(bulletPerfab, shootPos.position, Quaternion.Euler(0, 0, angle));

        yield return new WaitForSeconds(reloadTime);

        StartCoroutine(LaunchBacon(reloadTime));
    }
}
