using UnityEngine;
using System.Collections;

public class SwitchCollision : MonoBehaviour {

	// Use this for initialization
	void Start()
    {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" || collider.tag == "Enemy")
        {
            Debug.Log("Turning OFF collision");
            Physics2D.IgnoreCollision(collider, this.collider2D, true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player" || collider.tag == "Enemy")
        {
            Debug.Log("Turning ON collision");
            Physics2D.IgnoreCollision(collider, this.collider2D, false);
        }
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
