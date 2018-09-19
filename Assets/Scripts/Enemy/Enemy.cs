using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject slash;
    public GameObject stab;
    public GameObject upSlash;

    [Tooltip("The amount of health the enemy has")]
    [SerializeField] private int _health = 20;

    [SerializeField] private Vector2 _knockBackspeed;
    [SerializeField] private float _flyupSpeed = 0.2f;

    [Tooltip("Whether or not the enemy is immune to knockup attacks")]
    [SerializeField] private bool _knockUpImmune;

    float _knockSpeedX;
    float _knockSpeedY;
    float _flightSpeed;
    private EnemyStates _state;
    bool _fly;
    bool _knockBack;

    private float _startY;
    Vector3 _oldPosition;

    private EnemyAnimation _animation;
    private Rigidbody _playerRigidbody;

    void Start()
    {
        EnemyHandler.Instance.EnemySpawned(gameObject);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerRigidbody = player.GetComponent<Rigidbody>();

        _animation = GetComponent<EnemyAnimation>();
        _state = GetComponent<EnemyStates>();
        _startY = transform.position.y;
    }

    void Update()
    {
        if (_fly)
        {
            _flightSpeed -= 0.01f;

            if (_state.CurrentState == EnemyStates.EnemyState.FLYUP)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + _flightSpeed, transform.position.z);

                CancelInvoke();
            }

            if (transform.position.y <= _oldPosition.y)
            {
                transform.position = new Vector3(transform.position.x, _startY, transform.position.z);
                _fly = false;
                Invoke("changeStateRandom", 0.5f);

                //Spawn the dust under the guy
                //float enemyHeight = GetComponent<SpriteRenderer>().sprite.texture.height;
                //float dustHeight = dust.GetComponent<SpriteRenderer>().sprite.texture.height;
                //float y = transform.position.y - (enemyHeight - dustHeight) / 200f;
                //Instantiate(dust, new Vector3(transform.position.x, y, transform.position.z - 0.1f), Quaternion.identity);
            }

            if (_state.CurrentState == EnemyStates.EnemyState.DAMAGED || _state.CurrentState == EnemyStates.EnemyState.AIRDAMAGED)
            {
                _flightSpeed = 0.05f;
                _flightSpeed += 0.03f;
            }
        }
        if (_knockBack)
        {
            if (_state.CurrentState != EnemyStates.EnemyState.DAMAGED && _state.CurrentState != EnemyStates.EnemyState.AIRDAMAGED)
            {
                if (!GetComponent<SpriteRenderer>().flipX)
                    transform.position = new Vector3(transform.position.x + _knockSpeedX, transform.position.y + _knockSpeedY, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x - _knockSpeedX, transform.position.y + _knockSpeedY, transform.position.z);

                CancelInvoke();
            }

            if (transform.position.y <= _oldPosition.y)
            {
                transform.position = new Vector3(transform.position.x, _startY, transform.position.z);
            }
            else
            {
                _knockSpeedY -= 0.01f;
            }
            _knockSpeedX -= 0.01f;

            if (_knockSpeedX <= 0)
            {
                _knockBack = false;
                Invoke("changeStateRandom", 0.5f);
            }
        }
    }

    public bool Hit(int pDamage)
    {
        if (_state.CurrentState == EnemyStates.EnemyState.DEAD)
        {
            return false;
        }

        CancelInvoke();

        if (_state.CurrentState == EnemyStates.EnemyState.FLYUP || _state.CurrentState == EnemyStates.EnemyState.AIRDAMAGED)
        {
            _fly = true;
            _state.ChangeState(EnemyStates.EnemyState.AIRDAMAGED);
        }
        else
        {
            _state.ChangeState(EnemyStates.EnemyState.DAMAGED);
        }

        _health -= pDamage;

        if (_health <= 0)
        {
            startDying();
            return true;
        }

        if (_fly)
        {
            Invoke("changeStateFly", 0.1f);
            transform.Translate(0, 0.3f, 0);
        }
        else
        {
            Invoke("changeStateRandom", 0.5f);
        }

        return false;
    }

    public void Fly()
    {
        if (_knockUpImmune || _state.CurrentState == EnemyStates.EnemyState.FLYUP || _state.CurrentState == EnemyStates.EnemyState.DEAD)
        {
            return;
        }

        _state.ChangeState(EnemyStates.EnemyState.FLYUP);
        _fly = true;
        _oldPosition = transform.position;
        _flightSpeed = _flyupSpeed;
    }

    public void KnockBack()
    {
        if ((_state.CurrentState != EnemyStates.EnemyState.FLYUP && _state.CurrentState != EnemyStates.EnemyState.AIRDAMAGED) || _state.CurrentState == EnemyStates.EnemyState.DEAD)
        {
            return;
        }

        _state.ChangeState(EnemyStates.EnemyState.FLYUP);
        _animation.KnockUpAnimation();
        _knockBack = true;
        _knockSpeedX = _knockBackspeed.x;
        _knockSpeedY = _knockBackspeed.y;
        if (!_fly)
        {
            _oldPosition = transform.position;
        }
    }

    public void SpawnSlash(int pSlash)
    {
        switch (pSlash)
        {
            case 1:
                if (GetComponent<SpriteRenderer>().flipX)
                    Instantiate(slash, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f), Quaternion.identity);
                else
                    Instantiate(slash, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f), Quaternion.Euler(0, -180f, 0));
                break;
            case 2:
                Instantiate(stab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f), Quaternion.identity);
                break;
            case 3:
                Instantiate(upSlash, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 0.1f), Quaternion.identity);
                break;
            default:
                Instantiate(slash, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f), Quaternion.identity);
                break;
        }
    }

    private void startDying()
    {
        _state.ChangeState(EnemyStates.EnemyState.DEAD);
        _animation.DeathAnimation();
    }

    private void die()
    {
        EnemyHandler.Instance.EnemyDied(gameObject);
    }

    private void changeState()
    {
        _state.ChangeState(EnemyStates.EnemyState.MOVING);
    }

    private void changeStateFly()
    {
        _state.ChangeState(EnemyStates.EnemyState.FLYUP);
    }

    private void changeStateRandom()
    {
        int random = Random.Range(0, 2);
        EnemyHandler.Instance.Attacked(gameObject);

        switch (random)
        {
            case 0:
                _state.ChangeState(EnemyStates.EnemyState.SURROUNDING);
                break;
            case 1:
                _state.ChangeState(EnemyStates.EnemyState.RETREAT);
                break;
            default:
                _state.ChangeState(EnemyStates.EnemyState.SURROUNDING);
                break;
        }
    }

    public void UnFreezeAnimations()
    {
        _animation.ResumeAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            List<GameObject> enemies = other.GetComponent<Attack>().EnemiesInRange;

            if (!enemies.Contains(gameObject))
            {
                enemies.Add(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Vector3.Distance(transform.position, _playerRigidbody.position) < 2)
            {
                return;
            }
            other.GetComponent<Attack>().EnemiesInRange.Remove(gameObject);
        }
    }

    //Parameters
    public int Health
    {
        set
        {
            _health = value;
        }
    }

    public Vector2 KnockBackSpeed
    {
        set
        {
            _knockBackspeed = value;
        }
    }

    public float FlyUpSpeed
    {
        set
        {
            _flyupSpeed = value;
        }
    }

    //Parameters:
    public bool KnockUp
    {
        get
        {
            return _knockUpImmune;
        }
        set
        {
            _knockUpImmune = value;
        }
    }
}
