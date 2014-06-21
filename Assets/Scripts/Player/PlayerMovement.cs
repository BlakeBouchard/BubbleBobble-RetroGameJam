using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float horizontalAcceleration = 1.0f;
    public float horizontalVelocity = 5.0f;
    private float horizontalAxis;

    private bool jumpWanted;
    private bool onGround = false;

    public bool forceBased = false;

	// Use this for initialization
	void Start()
    {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Ground":
                if (!onGround)
                {
                    onGround = true;
                }
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update()
    {
        // Grab horizontal control from Unity
        horizontalAxis = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalAxis);
        if (!jumpWanted && Input.GetKeyDown("Jump"))
        {
            jumpWanted = true;
        }
	}

    void FixedUpdate()
    {
        if (forceBased)
        {
            rigidbody2D.AddForce(new Vector2(horizontalAxis * horizontalAcceleration, 0));
        }
        else
        {
            rigidbody2D.velocity = new Vector2(horizontalAxis * horizontalVelocity, 0);
        }

        if ()
    }
}
