using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float _speed = 0.75f;

    GameObject _canvas;
    CameraFollow _camera;
    private Vector3 _velocity;
    private SpriteRenderer _renderer;
    private Rigidbody _rigidbody;
    private PlayerAnimation _animation;

    private Vector3 horizontalMovement;
    private Vector3 verticalMovement;
    private bool _isJumping;

    float _spriteWidth;
    float _leftBorder;
    float _rightBorder;

    // Use this for initialization
    void Start()
    {
        _velocity = new Vector3(0, 0);
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _animation = GetComponent<PlayerAnimation>();

        horizontalMovement = new Vector3(_speed, 0);
        verticalMovement = new Vector3(0, 0, _speed * 3.5f);
       
        _spriteWidth = 307f / 2; //Half of the player sprite width
        
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _camera = Camera.main.GetComponent<CameraFollow>();
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

        CheckColisionBorder();

        //_rigidbody.velocity = Vector3.zero;

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

        if (!_animation.IsAttacking)
        {
            _rigidbody.velocity = _velocity;
        }

        _velocity.Set(0, 0, 0);
    }

    void CheckColisionBorder()
    {
        if (_camera.GetFight)
        {
            if (_rigidbody.position.x <= _leftBorder - _spriteWidth / 200f)
            {
                _rigidbody.position = new Vector3(_leftBorder - _spriteWidth / 200f, _rigidbody.position.y, _rigidbody.position.z);
            }
            if (_rigidbody.position.x >= _rightBorder + _spriteWidth / 200f)
            {
                _rigidbody.position = new Vector3(_rightBorder + _spriteWidth / 200f, _rigidbody.position.y, _rigidbody.position.z);
            }
        }
        else if (_camera.InBox)
        {
            Debug.Log("Ama here");
            if (_rigidbody.position.x <= _leftBorder - _spriteWidth / 200f)
            {
                _rigidbody.position = new Vector3(_leftBorder - _spriteWidth / 200f, _rigidbody.position.y, _rigidbody.position.z);
            }
            if (_rigidbody.position.x >= _rightBorder + _spriteWidth / 200f)
            {
                _camera.DisableInBox();
                _canvas.transform.Find("Arrow").gameObject.SetActive(false);

                if (_camera.FinalBox)
                {
                    _camera.ReachEndOfScene();
                }
            }
        }
    }

    public void SetBorders()
    {
        _leftBorder = _rigidbody.position.x - _canvas.GetComponent<RectTransform>().rect.width / 200f;
        _rightBorder = _rigidbody.position.x + _canvas.GetComponent<RectTransform>().rect.width / 200f;
    }
}
