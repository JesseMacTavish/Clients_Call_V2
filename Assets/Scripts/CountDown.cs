using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    private Text _text;
    private int _timer = 10;
    private float _time;
    private LevelTracker _player;

    void Start()
    {
        _text = GetComponent<Text>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>();
    }

    void Update()
    {
        _time += Time.deltaTime;
        int output = _timer - (int)_time;
        _text.text = output.ToString();

        if (output <= 0)
        {
            SceneManager.LoadScene("Menu_screen");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (_player.foughtThisLevel)
        {
            DecisionTracker.killer -= 2;
        }
        if (_player.exploredThisLevel)
        {
            DecisionTracker.explorer -= 2;
        }
        if (_player.foundJesterThisLevel)
        {
            DecisionTracker.explorer -= 1;
            DecisionTracker.socializer -= 3;
        }
        if (_player.foundJesterThisLevel && _player.foughtThisLevel && _player.exploredThisLevel)
        {
            DecisionTracker.achiever -= 4;
        }
    }
}
