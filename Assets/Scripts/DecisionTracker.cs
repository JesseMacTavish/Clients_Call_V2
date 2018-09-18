using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SetBool
{
    KILL_HITLER,
    TALKED_HITLER,
    LEFT_HITLER
}

public class DecisionTracker 
{
    public static float killer;
    public static float explorer;
    public static float achiever;
    public static float socializer;

    public static bool killedHitler;
    public static bool talkedHitler;
    public static bool leftHitler;

    public static void ToggleBool(SetBool pSetBool)
    {
        switch (pSetBool)
        {
            case SetBool.KILL_HITLER:
                killedHitler = true;
                break;
            case SetBool.TALKED_HITLER:
                talkedHitler = true;
                break;
            case SetBool.LEFT_HITLER:
                leftHitler = true;
                break;
            default:
                break;
        }
    }
}
