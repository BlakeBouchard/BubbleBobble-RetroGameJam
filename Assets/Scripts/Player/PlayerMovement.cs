using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float horizontalAcceleration = 1.0f;
    public float horizontalVelocity = 5.0f;
    private float horizontalAxis;

    public float jumpForce = 100.0f;
    public float jumpVelocity = 10.0f;

    public Animator animator;

    private bool jumpWanted = false;
    private bool jumpStarted = false;
    public float baseJumpTime = 1.0f;
    private float jumpTimeLeft = 0;

    public bool forceBased = false;

    private bool facingLeft = false;

    DetectSurface surfaceDetector;

	// Use this for initialization
	void Start()
    {
        surfaceDetector = GetComponentInChildren<DetectSurface>();
        if (!surfaceDetector)
        {
            Debug.LogError("ERROR! Could not find surface detection object in children!");
        }
	}

    private void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public bool IsFacingLeft()
    {
        return facingLeft;
    }

	// Update is called once per frame
	void Update()
    {
        // Grab horizontal control from Unity
        horizontalAxis = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalAxis);
        if (Input.GetButtonDown("Jump") && !jumpWanted)
        {
            //Debug.Log("Jump key pressed");
            jumpWanted = true;
        }

        if (jumpStarted && !Input.GetButton("Jump"))
        {
            jumpStarted = false;
        }
	}

    void FixedUpdate()
    {
        if (facingLeft && horizontalAxis > 0)
        {
            facingLeft = false;
            Flip();
        }
        else if (!facingLeft && horizontalAxis < 0)
        {
            facingLeft = true;
            Flip();
        }

        // If movement is force based, add horizontal force, otherwise, set horizontal velocity
        if (forceBased)
        {
            rigidbody2D.AddForce(new Vector2(horizontalAxis * horizontalAcceleration, 0));
        }
        else
        {
            rigidbody2D.velocity = new Vector2(horizontalAxis * horizontalVelocity, 0);
        }

        // The jump key is down so we should jump
        if (jumpWanted)
        {
            // Make sure the character is on the ground before jumping
            if (surfaceDetector.IsTouchingGround())
            {
                //Debug.Log("Character is on ground, so jump");
                jumpStarted = true;
                jumpTimeLeft = baseJumpTime;
            }
            jumpWanted = false;
        }

        // If the character is trying to jump, set the upward velocity to the jump velocity
        if (jumpStarted)
        {
            if (jumpTimeLeft > 0)
            {
                //Debug.Log("Jump time left: " + jumpTimeLeft);
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
                jumpTimeLeft -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("Ended Jump");
                jumpStarted = false;
            }
        }

        // Update the animator
        if (animator)
        {
            animator.SetBool("Walking", horizontalAxis != 0);
            if (surfaceDetector.IsTouchingGround())
            {
                animator.SetFloat("VelocityY", 0);
            }
            else
            {
                animator.SetFloat("VelocityY", rigidbody2D.velocity.y);
            }
        }
    }
}