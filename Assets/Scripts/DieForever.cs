using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieForever : MonoBehaviour
{
    void Start()
    {
        if (DecisionTracker.killedJester)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        DecisionTracker.killedJester = true;
        DecisionTracker.killer += 1;
        DecisionTracker.socializer -= 6;

        if (DecisionTracker.socializer < 0)
        {
            DecisionTracker.socializer = 0;
        }
    }
}
