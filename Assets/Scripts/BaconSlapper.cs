using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaconSlapper : Bacon
{
    public float rotationSpeed;

    private void FixedUpdate()
    {
        if (paused)
        { return; }

        rb.MoveRotation(rb.rotation + rotationSpeed);
    }
}
