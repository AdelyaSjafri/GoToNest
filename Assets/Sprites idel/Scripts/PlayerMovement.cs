using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    private bool isInAir;
    private bool canDoubleJump;


    private Animator animator; // Declare an Animator variable

    void Start()
    {
        animator = GetComponent<Animator>(); // Assign the Animator component to the animator variable
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;
            animator.SetBool("isJumping", false); // Set the "isJumping" parameter to false when grounded
            isInAir = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isInAir = true;
            jumpTimeCounter = jumpTime;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true); // Set the "isJumping" parameter to true when jumping
        }

        if (Input.GetButton("Jump") && isInAir == true)
        {
            if (jumpTimeCounter > 0) 
            {
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isInAir = false;
            }
        }
        
        if (Input.GetButtonUp("Jump"))
        {
            isInAir = false;
        }

        if (!isGrounded && Input.GetButtonDown("Jump") && canDoubleJump)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
        }

        // Update animator parameters
        animator.SetFloat("xVelocity", Mathf.Abs(input)); // Set the "xVelocity" parameter to the absolute value of the input
        animator.SetBool("isJumping", isInAir); // Set the "isJumping" parameter to the isInAir value
    }

    void FixedUpdate()
    {
        playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);
    }

    

    
}