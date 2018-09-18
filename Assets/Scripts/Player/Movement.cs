using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float _speed = 0.75f;

    private Vector3 _velocity;
    private Camera mainCamera;
    private Vector3 cameraPos;
    private SpriteRenderer _renderer;
    private Rigidbody _rigidbody;
    private PlayerAnimation _animation;

    private Vector3 horizontalMovement;
    private Vector3 verticalMovement;
    private bool _isJumping;

    // Use this for initialization
    void Start()
    {
        _velocity = new Vector3(0, 0);
        mainCamera = Camera.main;
        cameraPos = mainCamera.transform.position;
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _animation = GetComponent<PlayerAnimation>();

        horizontalMovement = new Vector3(_speed, 0);
        verticalMovement = new Vector3(0, 0, _speed * 3.5f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!_animation.IsAttacking)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                if (_renderer.flipX)
                {
                    _renderer.flipX = false;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                if (!_renderer.flipX)
                {
                    _renderer.flipX = true;
                }
            }
        }

        _velocity += Input.GetAxisRaw("Horizontal") * horizontalMovement;
        _velocity += Input.GetAxisRaw("Vertical") * verticalMovement;

        if (_velocity.magnitude == 0)
        {
            _animation.StopWalking();
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _animation.WalkAnimation();
        }

        addVelocity();
    }

    private void addVelocity()
    {
        if (_velocity.magnitude > _speed * 3.5f)
        {
            _velocity.Normalize();
            _velocity *= _speed * 2.5f;
        }

        cameraPos.x = _rigidbody.position.x;
        mainCamera.transform.position = cameraPos;

        if (!_animation.IsAttacking)
        {
            _rigidbody.velocity += _velocity;
        }

        _velocity.Set(0, 0, 0);
    }
}
