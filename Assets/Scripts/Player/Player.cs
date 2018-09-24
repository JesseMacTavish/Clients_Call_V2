using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Current health")]
    [SerializeField] private int _health = 100;

    [Header("Health bar slider")]
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private GameObject _gameOverScreen;

    private PlayerAnimation _animation;
    private Attack _attack;

    private void Start()
    {
        _attack = GetComponent<Attack>();
        _animation = GetComponent<PlayerAnimation>();
    }

    public void Hit(int pDamage)
    {
        _health -= pDamage * DecisionTracker.Difficulty;
        _healthbar.UpdateHealthBar(_health);

        _animation.DamageAnimation();

        _animation.FreezeAnimations();
        _attack.Invoke("UnFreezeAnimations", _attack.freezeTime);

        if (_health <= 0)
        {
            Invoke("Die", 0.3f);
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }
    }

    void Die()
    {
        _gameOverScreen.SetActive(true);
        Destroy(gameObject);
    }
}
