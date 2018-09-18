using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamaged : MonoBehaviour
{
    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void DamageAnimation()
    {
        _animator.Play("EnemyDamage");
    }

    public void DamageAirAnimation()
    {
        _animator.Play("EnemyDamageAir");
    }
}
