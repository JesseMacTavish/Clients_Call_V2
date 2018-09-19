using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetExplorerPoints : MonoBehaviour
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
            DecisionTracker.explorer += 2;
            GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().exploredThisLevel = true;

            if (_player.foughtThisLevel && _player.foundJesterThisLevel)
            {
                DecisionTracker.achiever += 4;
            }

            Destroy(gameObject);
        }
    }
}
