using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    IMP,
    BAT,
    BATVAR,
    WRAITH,
    WRAITHVAR,
    MOS,
    MOSVAR,
    FISH,
    FISHVAR,
    JOKER,
}

public class EnemyTypes : MonoBehaviour
{
    [Header("EnemyType")]
    [Tooltip("The type of the enemy (all parameters are based on this)")]
    [SerializeField] private EnemyType _type;

    private Enemy _enemy;
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;
    private EnemyDashing _enemyDashing;
    private EnemyRetreat _enemyRetreat;
    private EnemyStates _enemyStates;

    private JsonEnemyTypes _enemyTypes;

    // Use this for initialization
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyDashing = GetComponent<EnemyDashing>();
        _enemyRetreat = GetComponent<EnemyRetreat>();

        _enemyTypes = JsonEnemyTypes.Instance;
        setParameters(_type);
    }

    private void setParameters(EnemyType pType)
    {
        EnemyParameters enemyInfo = null;

        switch (pType)
        {
            case EnemyType.IMP:
                enemyInfo = _enemyTypes.IMP.EnemyParameters;
                break;
            case EnemyType.BAT:
                enemyInfo = _enemyTypes.BAT.EnemyParameters;
                break;
            case EnemyType.BATVAR:
                enemyInfo = _enemyTypes.BATVAR.EnemyParameters;
                break;
            case EnemyType.WRAITH:
                enemyInfo = _enemyTypes.WRAITH.EnemyParameters;
                break;
            case EnemyType.WRAITHVAR:
                enemyInfo = _enemyTypes.WRAITHVAR.EnemyParameters;
                break;
            case EnemyType.MOS:
                enemyInfo = _enemyTypes.MOS.EnemyParameters;
                break;
            case EnemyType.MOSVAR:
                enemyInfo = _enemyTypes.MOSVAR.EnemyParameters;
                break;
            case EnemyType.FISH:
                enemyInfo = _enemyTypes.FISH.EnemyParameters;
                break;
            case EnemyType.FISHVAR:
                enemyInfo = _enemyTypes.FISHVAR.EnemyParameters;
                break;
            case EnemyType.JOKER:
                enemyInfo = _enemyTypes.JOKER.EnemyParameters;
                break;
            default:
                break;
        }

        _enemy.KnockUp = enemyInfo.KnockUpImmune;

        _enemy.Health = enemyInfo.Health;
        _enemy.KnockBackSpeed = enemyInfo.KnockBackSpeed;
        _enemy.FlyUpSpeed = enemyInfo.FlyUpSpeed;

        _enemyAttack.Damage = enemyInfo.Damage;
        _enemyAttack.AttackRange = enemyInfo.AttackRange;
        _enemyAttack.FreezeTime = enemyInfo.FreezeTime;

        _enemyMovement.Speed = enemyInfo.Speed;
        _enemyMovement.SurroundDistance = enemyInfo.SurroundDistance;
        _enemyMovement.CanDash = enemyInfo.CanDash;
        _enemyMovement.CanShoot = enemyInfo.CanShoot;

        _enemyDashing.DashFrames = enemyInfo.DashFrames;
        _enemyDashing.StopDistance = enemyInfo.DashStopDistance;

        _enemyRetreat.RetreatSpeed = enemyInfo.RetreatSpeed;
        _enemyRetreat.RetreatDistance = enemyInfo.RetreatDistance;
    }

    public EnemyType CurrentType
    {
        get
        {
            return _type;
        }
    }
}
