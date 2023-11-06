using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float currentSpeed = 0;
    float moveX;
    float moveY;

    public float acceleration = 2f;
    public float deceleration = 2f;

    public Vector3 moveDirection;
    private Vector3 lastRecorderDir;


    [SerializeField] Rigidbody2D rb;

    
    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();   
    }

    public void Awake()
    {
        

    }
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    void Move()
    { 

        Vector3 direction = new Vector3(moveX, moveY, 0f).normalized;

        if (direction.magnitude >= 0.1f)
        {
            
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > moveSpeed)
            {
                currentSpeed = moveSpeed;
            }
            rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);

            lastRecorderDir = direction;
        }

        else
        {
            
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed < 0)
            {
                currentSpeed = 0;
            }
            rb.MovePosition(transform.position + lastRecorderDir * currentSpeed * Time.deltaTime);
        }
    }

}