using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    DEFAULT,
    FLYING,
    TYPE3,
    TYPE4,
}

public class EnemyTypes : MonoBehaviour
{
    [Header("EnemyType")]
    [Tooltip("The type of the enemy (all parameters are based on this)")]
    public EnemyType Type;

    private Enemy _enemy;
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;
    private EnemyRetreat _enemyRetreat;
    private EnemyStates _enemyStates;

    private JsonEnemyTypes _enemyTypes;

    // Use this for initialization
    void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyRetreat = GetComponent<EnemyRetreat>();

        _enemyTypes = JsonEnemyTypes.Instance;
        setParameters(Type);
    }

    private void setParameters(EnemyType pType)
    {
        EnemyParameters enemyInfo = null;

        switch (pType)
        {
            case EnemyType.DEFAULT:
                enemyInfo = _enemyTypes.EnemyDefault.EnemyParameters;
                break;
            case EnemyType.FLYING:
                enemyInfo = _enemyTypes.EnemyFlying.EnemyParameters;
                break;
            case EnemyType.TYPE3:
                break;
            case EnemyType.TYPE4:
                break;
            default:
                break;
        }

        _enemy.Health = enemyInfo.Health;
        _enemy.KnockBackSpeed = enemyInfo.KnockBackSpeed;
        _enemy.FlyUpSpeed = enemyInfo.FlyUpSpeed;

        _enemyAttack.Damage = enemyInfo.Damage;
        _enemyAttack.AttackRange = enemyInfo.AttackRange;
        _enemyAttack.FreezeTime = enemyInfo.FreezeTime;

        _enemyMovement.Speed = enemyInfo.Speed;
        _enemyMovement.SurroundDistance = enemyInfo.SurroundDistance;

        _enemyRetreat.RetreatSpeed = enemyInfo.RetreatSpeed;
        _enemyRetreat.RetreatDistance = enemyInfo.RetreatDistance;
    }
}
