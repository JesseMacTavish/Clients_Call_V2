using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _velocity;
    private Vector3 _accelleration;
    private float _speed = 500;

    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<Transform>();
        _accelleration = new Vector3(0, 0);
        _velocity = new Vector3(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _accelleration += new Vector3(1, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _accelleration += new Vector3(-1, 0);
        }

        addVelocity();
    }

    private void addVelocity()
    {
        _velocity += _accelleration;

        if (_velocity.magnitude > _speed)
        {
            _velocity.Normalize();
            _velocity *= _speed;
        }

        _transform.position += _velocity;

        _accelleration.Set(0, 0, 0);
        _velocity.Set(0, 0, 0);

    }
}
