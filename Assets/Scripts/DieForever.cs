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
    }
}
