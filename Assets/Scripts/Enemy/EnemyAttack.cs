using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [Tooltip("The time IN SECONDS between 2 enemy updates")]
    [SerializeField] private float _updateInterval = 0.5f;

    [Tooltip("The damage the enemy does")]
    [SerializeField] private int _damage = 10;

    [Tooltip("The range in which the enemy can hit you")]
    [SerializeField] private float _attackrange = 2;

    [Tooltip("The time IN SECONDS that the enemy will be frozen")]
    [SerializeField] private float _freezeTime = 0.1f;

    [HideInInspector] public ScreenShake ScreenShake;

    [Space(10)]
    public GameObject Projectile;

    private EnemyStates _state;
    private EnemyMovement _enemyMovement;
    private EnemyAnimation _animation;
    private EnemyTypes _enemyTypes;
    private SpriteRenderer _renderer;

    private Player _player;

    private float _time;
    private bool _attacked;
    private bool _attacking;

    // Use this for initialization
    void Start()
    {
        _animation = GetComponent<EnemyAnimation>();
        _state = GetComponent<EnemyStates>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyTypes = GetComponent<EnemyTypes>();
        _renderer = GetComponent<SpriteRenderer>();

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ScreenShake = Camera.main.GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state.CurrentState == EnemyStates.EnemyState.ATTACKING)
        {
            if (!_attacking)
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
    }

    private void intervalUpdate()
    {
        if (!_attacked)
        {
            startAttack();
            return;
        }

        _attacked = false;
        changeState();
    }

    private void attack()
    {
        _attacked = true;
        _attacking = false;

        if (_enemyMovement.CanShoot)
        {
            Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<MoveProjectile>().Damage = Damage;
            return;
        }

        moveForward();

        if (InReach)
        {
            _player.Hit(_damage);

            _animation.FreezeAnimation();
            Invoke("UnFreezeAnimations", _freezeTime);

            StartCoroutine(ScreenShake.Shake(0.1f, 0.1f));
        }
    }

    private void startAttack()
    {
        _animation.AttackAnimation();
        _attacking = true;
    }

    private void moveForward()
    {
        switch (_enemyTypes.CurrentType)
        {
            case EnemyType.IMP:
                break;
            case EnemyType.BAT:
            case EnemyType.BATVAR:
                break;
            case EnemyType.WRAITH:
            case EnemyType.WRAITHVAR:
                break;
            case EnemyType.MOS:
            case EnemyType.MOSVAR:
                break;
            case EnemyType.FISH:
            case EnemyType.FISHVAR:
                if (_renderer.flipX)
                {
                    transform.position += new Vector3(0.5f, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(-0.5f, 0, 0);
                }
                break;
            default:
                break;
        }
    }

    private EnemyStates.EnemyState randomState()
    {
        int random = Random.Range(0, 2);

        switch (random)
        {
            case 0:
                return EnemyStates.EnemyState.MOVING;
            case 1:
                return EnemyStates.EnemyState.RETREAT;
            default:
                return EnemyStates.EnemyState.MOVING;
        }
    }

    private void changeState()
    {
        EnemyHandler.Instance.Attacked(gameObject);
        _state.ChangeState(EnemyStates.EnemyState.RETREAT);
    }

    public bool InReach
    {
        get
        {
            return ((_player.GetComponent<Rigidbody>().position - transform.position).magnitude <= _attackrange);
        }
    }

    public void UnFreezeAnimations()
    {
        _animation.ResumeAnimation();
    }


    //Parameters:
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    public float AttackRange
    {
        get
        {
            return _attackrange;
        }
        set
        {
            _attackrange = value;
        }
    }

    public float FreezeTime
    {
        get
        {
            return _freezeTime;
        }
        set
        {
            _freezeTime = value;
        }
    }
}
