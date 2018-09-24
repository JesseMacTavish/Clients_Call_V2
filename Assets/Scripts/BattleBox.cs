using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleBox : MonoBehaviour
{
    [SerializeField]
    private GameObject wave;
    public bool isTransition;
    public Object sceneToLoad;
    public int sceneToLoadID;
    public GameObject afterTalk;

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
            if (afterTalk != null)
            {
                afterTalk.SetActive(true);
            }

            _camera.StopFight();

            _canvas.transform.Find("Arrow").gameObject.SetActive(true);

            if (afterTalk == null)
                Destroy(gameObject);          
        }

        if (_camera.EndOfScene)
        {
            SceneManager.LoadScene(sceneToLoadID);
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
