using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float horizontalAcceleration = 1.0f;
    public float horizontalVelocity = 5.0f;

    public bool forceBased = false;

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalAxis);
        if (forceBased)
        {
            rigidbody2D.AddForce(new Vector2(horizontalAxis * horizontalAcceleration, 0));
        }
        else
        {
            rigidbody2D.velocity = new Vector2(horizontalAxis * horizontalVelocity, 0);
        }
	}
}
