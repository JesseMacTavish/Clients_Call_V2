using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Tooltip("The range in which you will hit enemies")]
    public float Attackrange = 20;

    [Tooltip("The damage of a standard single attack")]
    public int DefaultDamage = 10;

    [Tooltip("LeapLength, leapHeight\nActual leap is 2x longer than LeapLength")]
    public Vector2 LeapLengthAndHeight;

    [Header("Just for you henrik!!!")]
    public Vector2 JUMP;

    private ScreenShake _screenShake;

    public float freezeTime = 0.1f;
    private PlayerAnimation _animation;
    private BoxCollider _trigger;
    private Rigidbody _rigidbody;

    private bool _pressedAttack;
    private int _combo = 0;

    private List<GameObject> _enemiesInRange;

    private bool _leaping;
    private bool _highestPoint;
    private Vector3 _oldPosition;
    private Vector3 _newPosition;
    private Vector3 _leapDirection;
    private float _value;
    private bool _jumping;

    private SpriteRenderer _renderer;

    void Start()
    {
        _animation = GetComponent<PlayerAnimation>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _enemiesInRange = new List<GameObject>();
        _trigger = GetComponent<BoxCollider>();
        _trigger.size = new Vector3(Attackrange, _trigger.size.y, Attackrange);
        _screenShake = Camera.main.GetComponent<ScreenShake>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, JUMP.y, 0), ForceMode.VelocityChange);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!_animation.IsAttacking)
            {
                _combo = 0;
            }
            else
            {
                _pressedAttack = true;
            }

            _animation.AttackAnimation();
        }

        if (_leaping)
        {
            leaping();
        }
    }

    private void continueAnimation()
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            if (_renderer.flipX)
            {
                _renderer.flipX = false;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            if (!_renderer.flipX)
            {
                _renderer.flipX = true;
            }
        }

        if (!_pressedAttack)
        {
            _combo = 0;
            _animation.StopAll();
            return;
        }

        _combo++;
        _pressedAttack = false;
    }

    public List<GameObject> EnemiesInRange
    {
        get
        {
            return _enemiesInRange;
        }
    }

    private void attack()
    {
        int damage = DefaultDamage;

        damage *= (_combo + 1);

        for (int i = 0; i < _enemiesInRange.Count; i++)
        {
            if (_enemiesInRange[i] == null)
            {
                _enemiesInRange.RemoveAt(i);
            }

            if (GetComponent<SpriteRenderer>().flipX)
            {
                Enemy enemy = _enemiesInRange[i].GetComponent<Enemy>();
                if (enemy.GetComponent<Transform>().position.x <= GetComponent<Rigidbody>().position.x)
                {
                    if (enemy.Hit(damage))
                    {
                        _enemiesInRange.RemoveAt(i);
                        i--;
                    }

                    GetComponent<Animator>().speed = 0;
                    enemy.GetComponent<Animator>().speed = 0;
                    Invoke("unFreezeAnimations", freezeTime);
                    enemy.Invoke("unFreezeAnimations", freezeTime);

                    StartCoroutine(_screenShake.Shake(0.1f, 0.03f));
                }
            }
            else
            {
                Enemy enemy = _enemiesInRange[i].GetComponent<Enemy>();
                if (enemy.GetComponent<Transform>().position.x >= GetComponent<Rigidbody>().position.x)
                {
                    if (enemy.Hit(damage))
                    {
                        _enemiesInRange.RemoveAt(i);
                        i--;
                    }

                    GetComponent<Animator>().speed = 0;
                    enemy.GetComponent<Animator>().speed = 0;
                    Invoke("unFreezeAnimations", freezeTime);
                    enemy.Invoke("unFreezeAnimations", freezeTime);

                    StartCoroutine(_screenShake.Shake(0.1f, 0.03f));
                }
            }
        }
    }

    private void throwEnemyUp()
    {
        if (getEnemiesInRange().Count > 0)
        {
            foreach (Enemy enemy in getEnemiesInRange())
            {
                enemy.Fly();
            }
        }
    }

    private void throwEnemyAway()
    {
        if (getEnemiesInRange().Count > 0)
        {
            foreach (Enemy enemy in getEnemiesInRange())
            {
                enemy.KnockBack();
            }
        }
    }

    private void makeSlash(int pSlash)
    {
        if (getEnemiesInRange().Count > 0)
        {
            foreach (Enemy enemy in getEnemiesInRange())
            {
                enemy.SpawnSlash(pSlash);
            }
        }
    }

    private void leap()
    {
        _oldPosition = transform.position;
        _newPosition = _oldPosition + (Vector3)LeapLengthAndHeight;
        _leapDirection = _newPosition - _oldPosition;
        if (GetComponent<SpriteRenderer>().flipX)
        {
            _leapDirection.x *= -1;
        }
        _newPosition = _oldPosition + _leapDirection;
        _value = 0;
        _highestPoint = false;
        _leaping = true;
    }

    private void leaping()
    {
        if (!_highestPoint)
        {
            _value += 1 / 3f;
        }
        else
        {
            _value += 1 / 2f;
        }

        _rigidbody.position = _oldPosition + _leapDirection * _value;

        if (_value >= 1)
        {
            _oldPosition = _rigidbody.position;
            _leapDirection.y *= -1;
            _value = 0;

            if (_highestPoint)
            {
                _leaping = false;
                return;
            }

            _highestPoint = true;
        }
    }

    List<Enemy> getEnemiesInRange()
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < _enemiesInRange.Count; i++)
        {
            Enemy enemy = _enemiesInRange[i].GetComponent<Enemy>();
            if (GetComponent<SpriteRenderer>().flipX)
            {
                if (enemy.GetComponent<Transform>().position.x <= GetComponent<Rigidbody>().position.x)
                {
                    enemies.Add(enemy);
                }
            }
            else
            {
                if (enemy.GetComponent<Transform>().position.x >= GetComponent<Rigidbody>().position.x)
                {
                    enemies.Add(enemy);
                }
            }
        }

        return enemies;
    }

    public void unFreezeAnimations()
    {
        GetComponent<Animator>().speed = 1;
    }
}
