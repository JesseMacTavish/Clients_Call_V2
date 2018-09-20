using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashing : MonoBehaviour
{
    [Tooltip("The amount of frames the dash has")]
    [SerializeField] private int _frames;

    [Tooltip("How far away from the player the enemy will stop dashing")]
    [SerializeField] private float _stopDistance = 0.2f;

    private EnemyAnimation _animation;
    private Rigidbody _playerRigidBody;
    private EnemyAttack _enemyAttack;
    private Vector3 _oldPosition;
    private Vector3 _dashDirection;
    private float _value;

    private EnemyStates _state;

    private bool _attacked;

    private Player _player;

    // Use this for initialization
    void Start()
    {
        _state = GetComponent<EnemyStates>();
        _animation = GetComponent<EnemyAnimation>();
        _enemyAttack = GetComponent<EnemyAttack>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerRigidBody = player.GetComponent<Rigidbody>();
        _player = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state.CurrentState == EnemyStates.EnemyState.DASHING)
        {
            dashing();
        }
    }

    public void Dash()
    {
        _attacked = false;
        _oldPosition = transform.position;
        _dashDirection = _playerRigidBody.position - _oldPosition;
        _dashDirection.y = 0;
        _value = 0;
        _animation.AttackAnimation();
    }

    private void dashing()
    {
        _value += 1f / _frames;

        transform.position = _oldPosition + _dashDirection * _value;

        if (!_attacked)
        {
            if (Vector3.Distance(transform.position, _playerRigidBody.position) <= _enemyAttack.AttackRange)
            {
                if (_enemyAttack.InReach)
                {
                    dashAttack();
                }
            }
        }

        if (_value >= 1 && Vector3.Distance(transform.position, _playerRigidBody.position) >= _stopDistance)
        {
            EnemyHandler.Instance.Attacked(gameObject);
            _state.ChangeState(EnemyStates.EnemyState.RETREAT);
        }
    }

    private void dashAttack()
    {
        _attacked = true;

        _player.Hit(_enemyAttack.Damage);
        
        _enemyAttack.Invoke("UnFreezeAnimations", _enemyAttack.FreezeTime);

        StartCoroutine(_enemyAttack.ScreenShake.Shake(0.1f, 0.1f));
    }

    //Parameters:
    public int DashFrames
    {
        set
        {
            _frames = value;
        }
    }

    public float StopDistance
    {
        set
        {
            _stopDistance = value;
        }
    }
}
