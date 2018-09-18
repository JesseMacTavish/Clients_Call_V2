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

    public void Hit(int pDamage)
    {
        _health -= pDamage;
        _healthbar.UpdateHealthBar(_health);

        if (_health <= 0)
        {
            //Die
            //GetComponent<PlayerAnimation>().DeathAnimation();
            //Destroy(gameObject);
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }
    }
}
