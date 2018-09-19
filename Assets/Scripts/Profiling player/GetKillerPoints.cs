using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKillerPoints : MonoBehaviour
{
    LevelTracker _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DecisionTracker.killer += 2;
            _player.foughtThisLevel = true;

            if (_player.exploredThisLevel && _player.foundJesterThisLevel)
            {
                DecisionTracker.achiever += 4;
            }

            Destroy(gameObject);
        }     
    }
}
