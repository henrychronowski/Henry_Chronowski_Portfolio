using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // * Variables *
    float xMove = 0f;
    public float moveSpeed = 5.0f;

    bool isJump = false;
    public float jumpSpeed = 10.0f;
    int jumpTime = 0;
    const int maxJumpTime = 2;
    public CheckGround groundCheck;

    Rigidbody2D rgbd;

    // ** Update Functions **
    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInputs();
        ManageJump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // **** Other Functions ****
    void GetInputs()
    {
        xMove = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.GetKey(KeyCode.Space) && groundCheck.grounded == true)
        {
            isJump = true;
            jumpTime = maxJumpTime;
        }
    }

    void ManageJump()
    {
        if (jumpTime > 0)
        {
            jumpTime--;
        } else
        {
            isJump = false;
        }
    }

    void Move()
    {
        Vector2 temp = rgbd.velocity;
        temp.x = xMove;

        if (isJump == true)
        {
            temp.y = jumpSpeed;
        }

        rgbd.velocity = temp;
    }
}
