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

    public void Hit(int pDamage)
    {
        _health -= pDamage * DecisionTracker.Difficulty;
        _healthbar.UpdateHealthBar(_health);

        if (_health <= 0)
        {
            Invoke("Die", 1f);
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
