using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonParser : MonoBehaviour
{
    [Tooltip("The name of the .json file")]
    public string FileName = "EnemyInfo.json";

    private string _path;
    public static string JsonString;

    private void Start()
    {
        _path = Application.streamingAssetsPath + "/" + FileName;
        JsonString = File.ReadAllText(_path);
    }
}

[System.Serializable]
public class JsonEnemyTypes
{
    public IMP IMP;
    public BAT BAT;
    public BATVAR BATVAR;
    public WRAITH WRAITH;
    public WRAITHVAR WRAITHVAR;
    public MOS MOS;
    public MOSVAR MOSVAR;
    public FISH FISH;
    public FISHVAR FISHVAR;

    public static JsonEnemyTypes Instance
    {
        get
        {
            return JsonUtility.FromJson<JsonEnemyTypes>(JsonParser.JsonString);
        }
    }
}

[System.Serializable]
public class IMP
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class BAT
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class BATVAR
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class WRAITH
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class WRAITHVAR
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class MOS
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class MOSVAR
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class FISH
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class FISHVAR
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class EnemyParameters
{
    public bool KnockUpImmune;

    public int Health;
    public Vector2 KnockBackSpeed;
    public float FlyUpSpeed;

    public int Damage;
    public float AttackRange;
    public float FreezeTime;
    public bool CanShoot;

    public float Speed;
    public Vector2 SurroundDistance;
    public bool CanDash;

    public int DashFrames;
    public float DashStopDistance;

    public float RetreatSpeed;
    public float RetreatDistance;
}
