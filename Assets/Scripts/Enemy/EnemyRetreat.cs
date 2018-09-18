using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRetreat : MonoBehaviour
{
    [Tooltip("The time IN SECONDS between 2 enemy updates")]
    [SerializeField] private float _updateInterval = 1;

    [Tooltip("The speed at which the enemy will retreat")]
    [SerializeField] private float _retreatSpeed = 0.1f;

    [Tooltip("The distance at which the enemy will stop retreating")]
    [SerializeField] private float _retreatDistance = 9;

    private Transform _transform;
    private EnemyStates _state;

    private GameObject _player;
    private Rigidbody _playerRigidbody;

    private float _time;
    private Vector3 _direction;

    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<Transform>();
        _state = GetComponent<EnemyStates>();

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigidbody = _player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state.CurrentState == EnemyStates.EnemyState.RETREAT)
        {
            if (_time >= _updateInterval)
            {
                intervalUpdate();
                _time = 0;
            }
            else
            {
                _time += 1 * Time.deltaTime;
            }

            retreat();
        }
    }

    private void intervalUpdate()
    {

    }

    public void NewDirection()
    {
        _direction = _transform.position - _playerRigidbody.position;
        _direction.z = 0;
        _direction.y = 0;

        int random = Random.Range(0, 2);
        random = (random == 0) ? -1 : random;

        _direction.x *= random;
    }

    private void retreat()
    {
        _transform.Translate(_direction.normalized * _retreatSpeed);

        if (Vector3.Distance(_transform.position, _playerRigidbody.position) >= _retreatDistance)
        {
            _state.ChangeState(EnemyStates.EnemyState.SURROUNDING);
        }
    }


    //Parameters:
    public float RetreatSpeed
    {
        set
        {
            _retreatSpeed = value;
        }
    }

    public float RetreatDistance
    {
        set
        {
            _retreatDistance = value;
        }
    }
}
