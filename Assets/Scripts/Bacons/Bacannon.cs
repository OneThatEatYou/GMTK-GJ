using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bacannon : MonoBehaviour
{
    public GameObject bulletPerfab;
    public Transform shootPos;
    public float reloadTime;
    public float angle;
    public float destroyDelay;

    List<GameObject> pooledBullets = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(LaunchBacon(reloadTime));
    }

    IEnumerator LaunchBacon(float reloadTime)
    {
        SpawnBullet();

        yield return new WaitForSeconds(reloadTime);

        StartCoroutine(LaunchBacon(reloadTime));
    }

    void SpawnBullet()
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                pooledBullets[i].transform.position = shootPos.position;
                pooledBullets[i].SetActive(true);

                return;
            }
        }

        GameObject obj = Instantiate(bulletPerfab, shootPos.position, Quaternion.Euler(0, 0, angle));
        obj.transform.SetParent(transform);
        pooledBullets.Add(obj);
    }
}
