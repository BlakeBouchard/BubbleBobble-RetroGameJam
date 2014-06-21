using UnityEngine;
using System.Collections;

public class DetectSurface : MonoBehaviour {
    
    public bool onGround = false;

	// Use this for initialization
	void Start()
    {
        
	}

    public bool IsTouchingGround()
    {
        return onGround;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Ground":
                if (!onGround)
                {
                    onGround = true;
                }
                break;
        }
    }

    void OnTrigger2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Ground":
                if (!onGround)
                {
                    onGround = true;
                }
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case Tags.Ground:
                if (onGround)
                {
                    onGround = false;
                }
                break;
        }
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
