using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float horizontalAcceleration = 1.0f;

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalAxis);
        rigidbody2D.AddForce(new Vector2(horizontalAxis * horizontalAcceleration, 0));
	}
}
