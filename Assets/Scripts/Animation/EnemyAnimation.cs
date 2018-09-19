using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider _collider;
    private EnemyType _type;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
        _type = GetComponent<EnemyTypes>().CurrentType;

        switch (_type)
        {
            case EnemyType.IMP:
                _collider.center = new Vector3(0, -1.5f, 0);
                _collider.size = new Vector3(4.5f, 6.5f, 2);
                break;
            case EnemyType.BAT:
            case EnemyType.BATVAR:
                _collider.center = new Vector3(-0.3f, 0.7f, 0);
                _collider.size = new Vector3(3.8f, 5, 2);
                break;
            case EnemyType.WRAITH:
            case EnemyType.WRAITHVAR:
                _collider.center = new Vector3(-0.5f, -0.6f, 0);
                _collider.size = new Vector3(3.5f, 8.3f, 2);
                break;
            case EnemyType.MOS:
            case EnemyType.MOSVAR:
                _collider.center = new Vector3(0, 0, 0);
                _collider.size = new Vector3(4, 3.5f, 2);
                break;
            case EnemyType.FISH:
            case EnemyType.FISHVAR:
                _collider.center = new Vector3(-0.5f, -1.3f, 0);
                _collider.size = new Vector3(8.5f, 6.5f, 2);
                break;
        }
    }

    public void WalkAnimation()
    {
        _animator.Play(_type + "Walk");
    }

    public void AttackAnimation()
    {
        _animator.Play(_type + "Attack");
    }

    public void KnockUpAnimation()
    {
        _animator.Play(_type + "Knockup");
    }

    public void KnockUpDamageAnimation()
    {
        _animator.Play(_type + "KnockupDamage");
    }

    public void DamageAnimation()
    {
        _animator.Play(_type + "Damage");
    }

    public void DeathAnimation()
    {
        _animator.Play(_type + "Death");
    }


    public void FreezeAnimation()
    {
        _animator.speed = 0;
    }

    public void ResumeAnimation()
    {
        _animator.speed = 1;
    }
}
