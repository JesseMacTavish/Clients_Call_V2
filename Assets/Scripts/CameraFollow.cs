using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 0.1f;
    private GameObject _player;
    private Vector3 _cameraPos;
    bool _freshFromTheFight;

    void Start()
    {
        _cameraPos = transform.position;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!InBox)
        {
            if (_freshFromTheFight)
            {
                _cameraPos.x += speed;
                transform.position = _cameraPos;

                if (_cameraPos.x > _player.transform.position.x)
                {
                    _cameraPos.x = _player.transform.position.x;
                    _freshFromTheFight = false;
                }
            }
            else
            {
                _cameraPos.x = _player.transform.position.x;
                transform.position = _cameraPos;
            }
        }
    }

    public void StartFight()
    {
        GetFight = true;
    }

    public void StopFight()
    {
        GetFight = false;
    }

    public void EnableInBox()
    {
        InBox = true;
    }

    public void DisableInBox()
    {
        InBox = false;
        _freshFromTheFight = true;
    }

    public void ActivateFinalBox()
    {
        FinalBox = true;
    }

    public void ReachEndOfScene()
    {
        EndOfScene = true;
    }

    public bool GetFight { get; private set; }
    public bool InBox { get; private set; }
    public bool FinalBox { get; private set; }
    public bool EndOfScene { get; private set; }
}
