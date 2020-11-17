using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float maxAirSpeed = 10f;
    public float jumpSpeed = 3f;
    public float jumpTime;
    public float swingForce = 4f;
    public float groundCheckDistance = 0.025f;
    public bool groundCheck;
    public bool isSwinging;
    public bool afterSwing;
    public bool canMove = true;
    //public bool timerMovement;
    //public float movementTimer;
    public Vector2 ropeHook;

    //[HideInInspector] 
    //public float moveTimeCounter = 0.0f;

    private float jumpTimeCounter = 0.0f;
    private bool isJumping;
    private float horizontalDirection;
    private float jumpInput;
    private Rigidbody2D rb2D;
    private SpriteRenderer playerSprite;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpInput = Input.GetAxis("Jump");
        isJumping = jumpInput > 0f;
        horizontalDirection = Input.GetAxis("Horizontal");
        //Debug.Log(rb2D.velocity);
    }

    private void FixedUpdate()
    {
        float halfHeight = playerSprite.bounds.extents.y;
        groundCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, groundCheckDistance);
        if (groundCheck)
        {
            canMove = true;
        }
        Move();
        Jump();
    }

    private void Move()
    {
        if (canMove)
        {
            if (horizontalDirection < 0f || horizontalDirection > 0f)
            {
                playerSprite.flipX = horizontalDirection < 0f;

                if (isSwinging)
                {
                    rb2D.AddForce(CalculatePerpendicularForce(), ForceMode2D.Force);
                }
                else
                {
                    if (groundCheck)
                        rb2D.velocity = new Vector2(horizontalDirection * speed, rb2D.velocity.y);
                    if (!groundCheck && !afterSwing)
                    {
                        rb2D.velocity = new Vector2(horizontalDirection * speed, rb2D.velocity.y);
                    }
                }
            }
        }
    }

    private void Jump()
    {
        if (!isSwinging)
        {
            if(isJumping && groundCheck)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                jumpTimeCounter = jumpTime;
            } 

            if (isJumping)
            {
                if (jumpTimeCounter > 0.0f)
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                    jumpTimeCounter -= Time.deltaTime;
                }
            }
        } 
    }

    Vector2 CalculatePerpendicularForce()
    {
        //Get a normalized direction vector from the player to the hook point
        Vector2 playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

        //Get Perpendicular direction
        Vector2 perpendicularDirection;
        if (horizontalDirection < 0)
        {
            perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
            Vector2 leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
            Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
        }
        else
        {
            perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
            Vector2 rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
            Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
        }

        Vector2 force = perpendicularDirection * swingForce;

        return force;
    }
}
