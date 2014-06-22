using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public bool facingLeft = true;

    public float enemySpeed = 5.0f;
    
    // Use this for initialization
	void Start()
    {
	
	}

    private void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        facingLeft = !facingLeft;
    }

    public void HitWall()
    {
        Debug.Log("Villain hit wall, should now flip");
        Debug.Log("Facing left is " + facingLeft + " and Rotation is " + transform.rotation.y);
        Flip();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);

        switch (collider.tag)
        {
            case Tags.Ground:
                HitWall();
                break;
        }
    }

    void FixedUpdate()
    {
        if (facingLeft)
        {
            rigidbody2D.velocity = new Vector2(-enemySpeed, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(enemySpeed, rigidbody2D.velocity.y);
        }
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}

    public UnityEngine.Object Prefab
    {
        get { return _prefab; }
    }

    [SerializeField]
    private UnityEngine.Object _prefab;
}
