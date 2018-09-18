using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleBox : MonoBehaviour
{
    public GameObject wave;
    public bool isTransition;

    public Object sceneToLoad;

    GameObject _canvas;
    CameraFollow _camera;

    void Start()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _camera = Camera.main.GetComponent<CameraFollow>();
    }

    void Update()
    {
        if (wave.transform.childCount == 0 && _camera.GetFight && !isTransition)
        {
            _camera.StopFight();
            Destroy(gameObject);
            _canvas.transform.Find("Arrow").gameObject.SetActive(true);
        }

        if (_camera.EndOfScene)
        {
            SceneManager.LoadScene(sceneToLoad.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_camera.GetFight)
        {
            _camera.StartFight();
            _camera.EnableInBox();
            wave.SetActive(true);
            other.GetComponent<Movement>().SetBorders();

            if (isTransition)
            {
                _camera.StopFight();
                _canvas.transform.Find("Arrow").gameObject.SetActive(true);
                _camera.ActivateFinalBox();
            }
        }
    }
}
