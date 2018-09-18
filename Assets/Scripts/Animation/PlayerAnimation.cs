using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void AttackAnimation()
    {
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        if (!IsAttacking)
        {
            _animator.Play("PlayerAttack");
        }
        else
        {
            if (currentState.IsName("PlayerAttack"))
            {
                _animator.Play("PlayerAttack2");
            }
        }
    }

    public bool IsAttacking
    {
        get
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            return (currentState.IsName("PlayerAttack") || (currentState.IsName("PlayerAttack2")));
        }
    }

    public void WalkAnimation()
    {
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        if (!currentState.IsName("PlayerWalk") && !IsAttacking)
        {
            _animator.Play("PlayerWalk");
        }
    }

    public void StopWalking()
    {
        if (!IsAttacking)
        {
            StopAll();
        }
    }

    public void DeathAnimation()
    {
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("PlayerDead"))
        {
            return;
        }

        _animator.Play("PlayerDead");
    }

    public void StopAll()
    {
        _animator.Play("PlayerIdle");
    }
}
