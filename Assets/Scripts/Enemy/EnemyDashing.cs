using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashing : MonoBehaviour
{
    [Tooltip("The amount of frames the dash has")]
    [SerializeField] private int _frames;

    private EnemyAnimation _animation;
    private Rigidbody _playerRigidBody;
    private Vector3 _oldPosition;
    private Vector3 _dashDirection;
    private float _value;

    private EnemyStates _state;

    // Use this for initialization
    void Start()
    {
        _state = GetComponent<EnemyStates>();

        _playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
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
        _oldPosition = transform.position;
        _dashDirection = _playerRigidBody.position - _oldPosition;
        _value = 0;
        _animation.AttackAnimation();
    }

    private void dashing()
    {
        _value += 1 / _frames;

        transform.position = _oldPosition + _dashDirection * _value;

        if (_value >= 1)
        {
            _state.ChangeState(EnemyStates.EnemyState.RETREAT);
        }
    }
}
