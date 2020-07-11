using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRelay : MonoBehaviour
{
    Bacon parentScript;

    private void Awake()
    {
        parentScript = GetComponentInParent<Bacon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parentScript.OnBaconCollide(collision);
    }
}
