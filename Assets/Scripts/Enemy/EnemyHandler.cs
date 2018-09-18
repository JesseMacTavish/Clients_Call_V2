using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    //TODO: make it so that you can only progress when you killed all enemies

    [Tooltip("The time IN SECONDS between 2 enemy updates")]
    [SerializeField] private float _updateInterval = 1;

    [Tooltip("The maximum amount of enemies that will attack at a time")]
    public int MaxAttackers = 2;

    private List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _readyToAttack = new List<GameObject>();

    private List<GameObject> _attackers = new List<GameObject>();

    private float _time;

    private bool _firstTime = true;
    private bool _update = false;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (_firstTime)
        {
            if (_readyToAttack.Count > 0)
            {
                UpdateAttackers();
                _firstTime = false;
            }
        }

        if (_update && _readyToAttack.Count > 0)
        {
            if (_time >= _updateInterval)
            {
                intervalUpdate();
                _time = 0;
                _update = false;
            }
            else
            {
                _time += 1 * Time.deltaTime;
            }
        }
    }

    private void intervalUpdate()
    {
        UpdateAttackers();
    }

    private void goAttack(GameObject pEnemy)
    {
        _attackers.Add(pEnemy);

        pEnemy.GetComponent<EnemyStates>().ChangeState(EnemyStates.EnemyState.MOVING);
    }

    public void Attacked(GameObject pEnemy)
    {
        _attackers.Remove(pEnemy);
        _update = true;
    }

    public static EnemyHandler Instance { get; private set; }


    public void EnemySpawned(GameObject pEnemy)
    {
        if (!_enemies.Contains(pEnemy))
        {
            _enemies.Add(pEnemy);
        }
    }

    public void EnemyDied(GameObject pEnemy)
    {
        pEnemy.GetComponent<EnemyMovement>().AddAvailableDegree();

        if (_enemies.Contains(pEnemy))
        {
            _enemies.Remove(pEnemy);
        }

        if (_readyToAttack.Contains(pEnemy))
        {
            _readyToAttack.Remove(pEnemy);
        }

        if (IsAttacker(pEnemy))
        {
            _attackers.Remove(pEnemy);
            _update = true;
        }

        Destroy(pEnemy);
    }

    public bool IsAttacker(GameObject pEnemy)
    {
        return _attackers.Contains(pEnemy);
    }

    public void Ready(GameObject pEnemy)
    {
        if (!_readyToAttack.Contains(pEnemy))
        {
            _readyToAttack.Add(pEnemy);
        }
    }


    public void UpdateAttackers()
    {
        while (_attackers.Count < MaxAttackers && _readyToAttack.Count > 0)
        {
            goAttack(_readyToAttack[0]);
            _readyToAttack.RemoveAt(0);
        }
    }
}
