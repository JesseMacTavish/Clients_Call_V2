using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("The time IN SECONDS between 2 enemy updates")]
    [SerializeField] private float _updateInterval = 1;

    [Tooltip("The speed at which the enemy will move towards the target")]
    [SerializeField] private float _speed = 0.1f;

    [Tooltip("The distance the enemy will stop from the target\nX = MinDistance, y = MaxDistance")]
    [SerializeField] private Vector2 _surroundDistance = new Vector2(5, 7);

    private static List<int> _availableDegreesRight = new List<int>() /*{ 0, 20, -20, 40, -40, 60, -60, };*/ { 60, 40, 20, 0, -20, -40, -60, };
    private static List<int> _availableDegreesLeft = new List<int>() /*{ 180, 160, 200, 140, 220, 120, 240, };*/ { 120, 140, 160, 180, 200, 220, 240, };

    private Transform _transform;
    private SpriteRenderer _renderer;
    private EnemyStates _state;

    private GameObject _player;
    private Rigidbody _playerRigidbody;

    private float _time;

    private Vector3 _target;
    private Vector3 _offSet;
    private int _degrees = int.MaxValue;

    private bool _surroundedPlayer;

    // Use this for initialization
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _state = GetComponent<EnemyStates>();

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRigidbody = _player.GetComponent<Rigidbody>();

        lookAtPlayer(_playerRigidbody.position);

        if (_state.CurrentState == EnemyStates.EnemyState.SURROUNDING)
        {
            NewTarget();
        }
    }

    private void Update()
    {
        lookAtPlayer(_playerRigidbody.position);

        if (_state.CurrentState == EnemyStates.EnemyState.SURROUNDING)
        {
            walkTowardsTarget();

            if (_surroundedPlayer)
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
            }
        }

        if (_state.CurrentState == EnemyStates.EnemyState.MOVING)
        {
            walkTowardsPlayer();
        }
    }

    private void intervalUpdate()
    {
        _surroundedPlayer = false;
        NewTarget();
    }

    private void walkTowardsTarget()
    {
        if (_surroundedPlayer)
        {
            return;
        }

        Vector3 direction = getTarget() - _transform.position;
        direction.y = 0;
        float distance = direction.magnitude;
        _transform.Translate(direction.normalized * _speed);

        distance -= _speed;

        if (distance <= 0)
        {
            _surroundedPlayer = true;
            EnemyHandler.Instance.Ready(gameObject);
        }
    }

    private void walkTowardsPlayer()
    {
        Vector3 direction = getTarget() - _transform.position;
        direction.y = 0;
        float distance = direction.magnitude;
        _transform.Translate(direction.normalized * _speed);

        distance -= _speed;

        if (distance <= 0)
        {
            _state.ChangeState(EnemyStates.EnemyState.ATTACKING);
        }
    }

    private Vector3 getTarget()
    {
        _target = _playerRigidbody.position + _offSet;

        return _target;
    }

    private Vector3 newOffset()
    {
        if (_state.CurrentState == EnemyStates.EnemyState.SURROUNDING)
        {
            getNextDegree();

            _offSet = GetUnitVectorDegrees(_degrees).normalized * Random.Range(_surroundDistance.x, _surroundDistance.y);
        }
        else if (_state.CurrentState == EnemyStates.EnemyState.MOVING)
        {
            _offSet = _offSet.normalized * (GetComponent<EnemyAttack>().AttackRange - 0.1f);
        }

        return _offSet;
    }

    private void getNextDegree()
    {
        if (_renderer.flipX)
        {
            if (_availableDegreesRight.Count > 0)
            {
                _degrees = _availableDegreesRight[0];
                _availableDegreesRight.Remove(_degrees);
            }
            else if (_availableDegreesLeft.Count > 0)
            {
                _degrees = _availableDegreesLeft[0];
                _availableDegreesLeft.Remove(_degrees);
            }
            else
            {
                _degrees = 0;
            }
        }
        else
        {
            if (_availableDegreesLeft.Count > 0)
            {
                _degrees = _availableDegreesLeft[0];
                _availableDegreesLeft.Remove(_degrees);
            }
            else if (_availableDegreesRight.Count > 0)
            {
                _degrees = _availableDegreesRight[0];
                _availableDegreesRight.Remove(_degrees);
            }
            else
            {
                _degrees = 180;
            }
        }
    }

    private Vector3 GetUnitVectorDegrees(float pDegrees)
    {
        float radians = Mathf.Deg2Rad * pDegrees;
        return new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
    }

    private void lookAtPlayer(Vector3 pPlayer)
    {
        if (pPlayer.x < _transform.position.x)
        {
            if (!_renderer.flipX)
            {
                _renderer.flipX = true;
            }
        }
        else
        {
            if (_renderer.flipX)
            {
                _renderer.flipX = false;
            }
        }
    }


    public void NewTarget()
    {
        AddAvailableDegree();

        _surroundedPlayer = false;

        newOffset();
    }

    public void AddAvailableDegree()
    {
        if (_degrees != int.MaxValue)
        {
            if (_renderer.flipX)
            {
                if (!_availableDegreesRight.Contains(_degrees))
                {
                    _availableDegreesRight.Add(_degrees);
                }
            }
            else
            {
                if (!_availableDegreesLeft.Contains(_degrees))
                {
                    _availableDegreesLeft.Add(_degrees);
                }
            }
        }

        _degrees = int.MaxValue;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            List<GameObject> enemies = other.GetComponent<Attack>().EnemiesInRange;

            if (!enemies.Contains(gameObject))
            {
                enemies.Add(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Vector3.Distance(transform.position, _playerRigidbody.position) < 2)
            {
                return;
            }
            other.GetComponent<Attack>().EnemiesInRange.Remove(gameObject);
        }
    }


    //Parameters:
    public float Speed
    {
        set
        {
            _speed = value;
        }
    }

    public Vector2 SurroundDistance
    {
        set
        {
            _surroundDistance = value;
        }
    }
}
