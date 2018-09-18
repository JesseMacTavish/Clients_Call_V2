using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamaged : MonoBehaviour
{
    private EnemyAnimation _animator;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<EnemyAnimation>();
    }

    public void DamageAnimation()
    {
        _animator.DamageAnimation();
    }

    public void DamageAirAnimation()
    {
        _animator.KnockUpDamageAnimation();
    }
}
