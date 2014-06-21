using UnityEngine;
using System.Collections;

public class PlayerShootBubbles : MonoBehaviour {

    public Transform bubblePrefab;
    public float timeBetweenShots = 0.02f;
    private float timeSinceLastShot = 0;

	// Use this for initialization
	void Start()
    {
	
	}

    // Create a bubble object at the character
    private void ShootBubble()
    {
        if (bubblePrefab)
        {
            Transform bubbleObject = Instantiate(bubblePrefab) as Transform;
            bubbleObject.name = bubblePrefab.name;
        }
    }
	
	// Update is called once per frame
	void Update()
    {
	    if (Input.GetButtonDown("Fire1"))
        {
            ShootBubble();
        }
	}
}
