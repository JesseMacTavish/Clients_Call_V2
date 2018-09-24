using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieForever : MonoBehaviour
{
    GameObject _player;

    void Start()
    {
        if (DecisionTracker.killedJester)
        {
            Destroy(gameObject);
        }

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnDestroy()
    {
        //If scene is being loaded or if player is actually attacking (god is going to cut me down for this)
        if (_player == null)
            return;
        if (!_player.GetComponent<PlayerAnimation>().IsAttacking || !_player.GetComponent<PlayerAnimation>().IsDashing)
            return;

        DecisionTracker.killedJester = true;
        DecisionTracker.killer += 1;
        DecisionTracker.socializer -= 6;

        if (DecisionTracker.socializer < 0)
        {
            DecisionTracker.socializer = 0;
        }
    }
}
