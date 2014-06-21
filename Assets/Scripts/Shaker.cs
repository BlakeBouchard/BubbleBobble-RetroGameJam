using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Shaker : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update() 
    {
        float displacement = _direction * _speed * Time.deltaTime;
        Value += displacement;
        if (Math.Abs(Value) > _amplitude / 2.0f)
        {
            _direction = -_direction;
        }
	}

    public float Value
    {
        get { return _value; }
        private set { _value = value; }
    }

    public void Reset()
    {
        _direction = 1.0f;
        _value = 0.0f;
    }

    [SerializeField]
    private float _amplitude;

    [SerializeField]
    private float _speed;

    private float _direction = 1.0f;
    private float _value;
}
