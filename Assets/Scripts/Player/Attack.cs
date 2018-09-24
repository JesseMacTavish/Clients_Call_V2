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

    [Tooltip("How far the dash is")]
    public float DashDistance;

    [HideInInspector] public ScreenShake screenShake;

    public List<GameObject> EnemiesInRange { get; private set; }

    public float freezeTime = 0.1f;
    private PlayerAnimation _animation;
    private BoxCollider _trigger;
    private Rigidbody _rigidbody;

    private bool _pressedAttack;
    private int _combo = 0;
    private bool _leaping;
    private bool _highestPoint;
    private Vector3 _oldPosition;
    private Vector3 _newPosition;
    private Vector3 _leapDirection;
    private float _value;
    private bool _dashing;
    private Vector3 _dashDirection;

    private SpriteRenderer _renderer;

    void Start()
    {
        _animation = GetComponent<PlayerAnimation>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody>();

        EnemiesInRange = new List<GameObject>();
        _trigger = GetComponent<BoxCollider>();
        _trigger.size = new Vector3(Attackrange, _trigger.size.y, Attackrange);
        screenShake = Camera.main.GetComponent<ScreenShake>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_animation.IsDashing)
            {
                return;
            }

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
        else if (Input.GetButtonDown("Fire2"))
        {
            if (!_animation.IsAttacking && !_animation.IsDashing)
            {
                dash();
            }
        }

        if (_leaping)
        {
            leaping();
        }

        if (_dashing)
        {
            dashing();
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



    private void attack() //todo: seriously, this can be easily improved
    {
        int damage = DefaultDamage;

        damage *= (_combo + 1);

        for (int i = 0; i < EnemiesInRange.Count; i++)
        {
            if (EnemiesInRange[i] == null)
            {
                EnemiesInRange.RemoveAt(i);
            }

            //Cheap but effective crutch
            Enemy enemy = null;
            try
            {
                enemy = EnemiesInRange[i].GetComponent<Enemy>();
            }
            catch
            {
                return;
            }

            if (_renderer.flipX)
            {
                if (enemy.GetComponent<Transform>().position.x <= GetComponent<Rigidbody>().position.x)
                {
                    if (enemy.Hit(damage))
                    {
                        EnemiesInRange.RemoveAt(i);
                        i--;
                    }

                    _animation.FreezeAnimations();
                    enemy.GetComponent<EnemyAnimation>().FreezeAnimation();
                    Invoke("UnFreezeAnimations", freezeTime);
                    enemy.Invoke("UnFreezeAnimations", freezeTime);

                    StartCoroutine(screenShake.Shake(0.1f, 0.03f));
                }
            }
            else
            {
                if (enemy.GetComponent<Transform>().position.x >= GetComponent<Rigidbody>().position.x)
                {
                    if (enemy.Hit(damage))
                    {
                        EnemiesInRange.RemoveAt(i);
                        i--;
                    }

                    _animation.FreezeAnimations();
                    enemy.GetComponent<EnemyAnimation>().FreezeAnimation();
                    Invoke("UnFreezeAnimations", freezeTime);
                    enemy.Invoke("UnFreezeAnimations", freezeTime);

                    StartCoroutine(screenShake.Shake(0.1f, 0.03f));
                }
            }
        }
    }

    private void dashAttack()
    {
        List<Enemy> enemies = getEnemiesInRange();
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];
            if (enemy.Hit(Damage))
            {
                EnemiesInRange.RemoveAt(i);
                i--;
            }

            enemy.GetComponent<EnemyAnimation>().FreezeAnimation();
            enemy.Invoke("UnFreezeAnimations", freezeTime);
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
        _oldPosition = _rigidbody.position;

        _leapDirection = LeapLengthAndHeight;
        if (GetComponent<SpriteRenderer>().flipX)
        {
            _leapDirection.x *= -1;
        }

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

    private void dash()
    {
        _animation.DashAnimation();

        _oldPosition = _rigidbody.position;
        if (_renderer.flipX)
        {
            _dashDirection = Vector3.left;
        }
        else
        {
            _dashDirection = Vector3.right;
        }

        _dashDirection *= DashDistance;

        _value = 0;

        _dashing = true;

        dashAttack();
    }

    private void dashing()
    {
        _value += 1 / 20f;

        _rigidbody.position = _oldPosition + _dashDirection * _value;

        if (_value >= 1)
        {
            _dashing = false;
            _animation.WalkAnimation();
        }
    }

    List<Enemy> getEnemiesInRange()
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < EnemiesInRange.Count; i++)
        {
            Enemy enemy = null;
            try
            {
                enemy = EnemiesInRange[i].GetComponent<Enemy>();
            }
            catch
            {
                continue;
            }

            if (_renderer.flipX)
            {
                if (enemy.GetComponent<Transform>().position.x <= _rigidbody.position.x)
                {
                    enemies.Add(enemy);
                }
            }
            else
            {
                if (enemy.GetComponent<Transform>().position.x >= _rigidbody.position.x)
                {
                    enemies.Add(enemy);
                }
            }
        }

        return enemies;
    }

    public void UnFreezeAnimations()
    {
        _animation.ResumeAnimation();
    }

    public int Damage
    {
        get
        {
            return DefaultDamage * (_combo + 1);
        }
    }

    void IncreaseHitBox()
    {
        _trigger.size = new Vector3(30, _trigger.size.y, 30);
    }

    void DecreaseHitBox()
    {
        _trigger.size = new Vector3(Attackrange, _trigger.size.y, Attackrange);
    }
}
