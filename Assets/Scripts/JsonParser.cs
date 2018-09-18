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

    private void Awake()
    {
        _path = Application.streamingAssetsPath + "/" + FileName;
        JsonString = File.ReadAllText(_path);
    }
}

[System.Serializable]
public class JsonEnemyTypes
{
    public EnemyDefault EnemyDefault;
    public EnemyFlying EnemyFlying;

    public static JsonEnemyTypes Instance
    {
        get
        {
            return JsonUtility.FromJson<JsonEnemyTypes>(JsonParser.JsonString);
        }
    }
}

[System.Serializable]
public class EnemyDefault
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class EnemyFlying
{
    public EnemyParameters EnemyParameters;
}

[System.Serializable]
public class EnemyParameters
{
    public int Health;
    public Vector2 KnockBackSpeed;
    public float FlyUpSpeed;
    public int Damage;
    public float AttackRange;
    public float FreezeTime;
    public float Speed;
    public Vector2 SurroundDistance;
    public float RetreatSpeed;
    public float RetreatDistance;
}
