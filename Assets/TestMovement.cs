using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float walkingSpeed;
    float x;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        //Move(x);
    }

    void Move(float horizontalInput)
    {
        rb.velocity = new Vector2(horizontalInput * walkingSpeed, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + x * walkingSpeed * Time.deltaTime);
    }
}
