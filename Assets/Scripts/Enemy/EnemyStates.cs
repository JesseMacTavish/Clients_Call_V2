using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    [Tooltip("The state of the enemy at the beginning")]
    [SerializeField] private EnemyState _startState = EnemyState.SURROUNDING;

    private EnemyAnimation _animation;

    public enum EnemyState
    {
        SURROUNDING,
        MOVING,
        ATTACKING,
        RETREAT,
        DAMAGED,
        FLYUP,
        AIRDAMAGED,
        DASHING,
        DEAD,
    }

    void Awake()
    {
        CurrentState = _startState;
        _animation = GetComponent<EnemyAnimation>();
    }

    private void Update()
    {
        //todo: be sure to get rid of this update method later
        _startState = CurrentState;
    }

    public void ChangeState(EnemyState pState)
    {
        CurrentState = pState;
        _animation.ResumeAnimation();

        switch (pState)
        {
            case EnemyState.MOVING:
            case EnemyState.SURROUNDING:
                GetComponent<EnemyMovement>().NewTarget();
                break;
            case EnemyState.ATTACKING:
                break;
            case EnemyState.RETREAT:
                GetComponent<EnemyRetreat>().NewDirection();
                break;
            case EnemyState.FLYUP:
                break;
            case EnemyState.DAMAGED:
                GetComponent<EnemyDamaged>().DamageAnimation();
                break;
            case EnemyState.AIRDAMAGED:
                GetComponent<EnemyDamaged>().DamageAirAnimation();
                break;
            case EnemyState.DASHING:
                GetComponent<EnemyDashing>().Dash();
                break;
            case EnemyState.DEAD:
                break;
            default:
                break;
        }
        
        GetComponent<EnemyMovement>().AddAvailableDegree();
    }

    public EnemyState CurrentState { get; private set; }

    public EnemyState StartState
    {
        set
        {
            _startState = value;
        }
    }
}
