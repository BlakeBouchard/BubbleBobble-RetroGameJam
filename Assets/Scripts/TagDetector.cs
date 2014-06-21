using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TagDetector : MonoBehaviour
{
    public void Clear()
    {
        _colliders.Clear();
    }

    public bool HasDetected
    {
        get { return _colliders.Count > 0; }
    }

    public List<Collider2D> Detectees
    {
        get { return _colliders; }
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
        if (collider.gameObject.tag == _tag)
        {
            _colliders.Add(collider);
        }
    }

    [SerializeField]
    private string _tag;
    private List<Collider2D> _colliders = new List<Collider2D>();
}

