using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [Tooltip("The distance per second the projectile travels")]
    [SerializeField] private float _speed = 0.2f;

    private static Rigidbody _playerRigidBody;
    private SpriteRenderer _renderer;

    private static Player _player;

    private Vector3 _direction;
    private bool _hit;

    // Use this for initialization
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _hit = false;

        if (_playerRigidBody == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) //true if player is destroyed
            {
                return;
            }
            _playerRigidBody = player.GetComponent<Rigidbody>();
            _player = player.GetComponent<Player>();
        }

        _direction = _playerRigidBody.position - transform.position;
        _direction.Normalize();

        lookAtPlayer(_playerRigidBody.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerRigidBody == null)
        {
            return;
        }

        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void lookAtPlayer(Vector3 pPlayer)
    {
        if (pPlayer.x < transform.position.x)
        {
            _renderer.flipX = false;
        }
        else
        {
            _renderer.flipX = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_hit)
            {
                return;
            }

            _player.Hit(Damage);

            _hit = true;

            Destroy(gameObject);
        }
    }

    public int Damage { get; set; }
}
