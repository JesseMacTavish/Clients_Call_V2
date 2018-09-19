using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SetBool
{
    KILL_HITLER,
    TALKED_HITLER,
    LEFT_HITLER,
    KILL_ANGELA,
    SAVE_ANGELA,
    KILL_OSCAR,
    SAVE_OSCAR,
    KILL_WILLIAM,
    SAVE_WILLIAM,
    ACCEPT_WILLIAM,
    REJECT_WILLIAM,
    ACCEPT_ELIZABETH,
    REJECT_ELIZABETH,
    KILL_ELIZABETH,
    SAVE_ELIZABETH,
    SACRIFICE_EDWIN,
    SACRIFICE_DAUGHTER,
    KILL_LORD,
    KILL_BOTH,
    SAVE_BOTH,
    KILL_E_SAVE_W,
    KILL_W_SAVE_E,
    GO_ALONE,
    GO_WITH
}

public class DecisionTracker : MonoBehaviour
{
    public static int Difficulty;

    public static float killer;
    public static float explorer;
    public static float achiever;
    public static float socializer;

    public static bool saveBoth;
    public static bool goWith;
    public static bool goAlone;
    public static bool acceptedElizabeth;
    public static bool rejectedElizabeth;
    public static bool eKilledwAlive;
    public static bool wKilledeAlive;
    public static bool killedBoth;
    public static bool killedHitler;
    public static bool talkedHitler;
    public static bool killedAngela;
    public static bool savedAngela;
    public static bool killedOscar;
    public static bool savedOscar;
    public static bool killedWilliam;
    public static bool savedWilliam;
    public static bool acceptedWilliam;
    public static bool rejectedWilliam;
    public static bool killedElizabeth;
    public static bool savedElizabeth;
    public static bool sacrificedEdwin;
    public static bool sacrificedDaughter;
    public static bool killedLord;

    public static bool killedJester;

    public static void ToggleBool(SetBool pSetBool)
    {
        switch (pSetBool)
        {
            case SetBool.SAVE_BOTH:
                saveBoth = true;
                break;
            case SetBool.GO_ALONE:
                goAlone = true;
                break;
            case SetBool.GO_WITH:
                goWith = true;
                break;
            case SetBool.ACCEPT_ELIZABETH:
                acceptedElizabeth = true;
                break;
            case SetBool.REJECT_ELIZABETH:
                rejectedElizabeth = true;
                break;
            case SetBool.KILL_E_SAVE_W:
                eKilledwAlive = true;
                break;
            case SetBool.KILL_W_SAVE_E:
                wKilledeAlive = true;
                break;
            case SetBool.KILL_BOTH:
                killedBoth = true;
                break;
            case SetBool.KILL_HITLER:
                killedHitler = true;
                break;
            case SetBool.TALKED_HITLER:
                talkedHitler = true;
                break;
            case SetBool.KILL_ANGELA:
                killedAngela = true;
                break;
            case SetBool.SAVE_ANGELA:
                savedAngela = true;
                break;
            case SetBool.KILL_OSCAR:
                killedOscar = true;
                break;
            case SetBool.SAVE_OSCAR:
                savedOscar = true;
                break;
            case SetBool.KILL_WILLIAM:
                killedWilliam = true;
                break;
            case SetBool.SAVE_WILLIAM:
                savedWilliam = true;
                break;
            case SetBool.ACCEPT_WILLIAM:
                acceptedWilliam = true;
                break;
            case SetBool.REJECT_WILLIAM:
                rejectedWilliam = true;
                break;
            case SetBool.KILL_ELIZABETH:
                killedElizabeth = true;
                break;
            case SetBool.SAVE_ELIZABETH:
                savedElizabeth = true;
                break;
            case SetBool.SACRIFICE_EDWIN:
                sacrificedEdwin = true;
                break;
            case SetBool.SACRIFICE_DAUGHTER:
                sacrificedDaughter = true;
                break;
            case SetBool.KILL_LORD:
                killedLord = true;
                break;
            default:
                break;
        }
    }
}
