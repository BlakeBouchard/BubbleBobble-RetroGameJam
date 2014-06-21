using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BubblePopDetector : MonoBehaviour
{
    public void Clear()
    {
        _playerColliders.Clear();
    }

    public bool ShouldPop
    {
        get { return _playerColliders.Count > 0; }
    }

    // Use this for initialization
    void Start()
    {
    }

	// Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case Tags.Player:
                Debug.Log("Collide Player");
                _playerColliders.Add(collider);
                break;
        }
    }

    [SerializeField]
    private Collider2D _collider;
    private List<Collider2D> _playerColliders = new List<Collider2D>();
}

