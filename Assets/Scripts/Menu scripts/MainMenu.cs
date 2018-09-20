using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public UnityEngine.Object sceneToLoad;

    public GameObject StartScreen;
    public GameObject[] DifficultyScreen;

    private bool _showedDifficulty;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject item in DifficultyScreen)
        {
            item.SetActive(false);
        }

        StartScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_showedDifficulty)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3"))
        {
            _showedDifficulty = true;

            StartScreen.SetActive(false);

            foreach (GameObject item in DifficultyScreen)
            {
                item.SetActive(true);
            }
        }
    }

    private void loadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad.name);
    }

    public void EasyDifficulty()
    {
        DecisionTracker.Difficulty = 1;
        loadNextScene();
    }

    public void HardDifficulty()
    {
        DecisionTracker.Difficulty = 2;
        loadNextScene();
    }

    public void NightmareDifficulty()
    {
        DecisionTracker.Difficulty = 3;
        loadNextScene();
    }
}
