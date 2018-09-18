using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandlerScript : MonoBehaviour
{
    float killer;
    float explorer;
    float achiever;
    float socializer;

    public void IncreaseKiller(float pValue)
    {
        killer += pValue;
        Debug.Log("Killer : " + killer);
    }

    public void IncreaseExplorer(float pValue)
    {
        explorer += pValue;
        Debug.Log("Explorer : " + explorer);
    }

    public void IncreaseAchiever(float pValue)
    {
        achiever += pValue;
        Debug.Log("Achiever : " + achiever);
    }

    public void IncreaseSocializer(float pValue)
    {
        socializer += pValue;
        Debug.Log("Socializer : " + socializer);
    }
}
