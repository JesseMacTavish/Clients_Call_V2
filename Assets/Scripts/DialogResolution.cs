using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DialogResolution : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject resScreen;
    public AudioSource audioSource;

    public List<GameObject> pictures = new List<GameObject>();
    public List<AudioClip> sounds = new List<AudioClip>();
    private List<string> dialogScript = new List<string>();

    public UnityEngine.Object defaultSceneToload;

    Text _dialog;
    int _dialogBoxID = -1;
    //Variables for slow type
    public float typeDelay = 0.03f;
    float _timeType;
    string _text = "";
    string _typedCharacters = "";

    void Start()
    {
        _dialog = dialogBox.GetComponentInChildren<Text>();

        if (true)
        {
            dialogScript.Add("There once was a castle filled with life.");
        }
        if (true)
        {
            dialogScript.Add("All was good and all was were happy.");
        }
       

        dialogScript.Add("Until the King killed all he could.");
        dialogScript.Add("Standing in blood he proudly said - ");
        dialogScript.Add("DIIIS NUTS!");

        Continue();
    }

    void Update()
    {
        Type();

        if (Input.GetButtonDown("Fire1"))
        {
            Continue();
        }
    }

    void Continue()
    {
        if (_typedCharacters.Length < _text.Length)
        {
            _typedCharacters = _text;
            return;
        }

        ClearText();

        if (_dialogBoxID < dialogScript.Count - 1)
        {
            _dialogBoxID++;
        }
        else
        {
            resScreen.SetActive(true);
        }

        string textLine = dialogScript[_dialogBoxID];

        //Add picture
        if (textLine.Contains("+"))
        {
            string _pictureID = textLine[textLine.IndexOf("+") + 1].ToString();
            pictures[int.Parse(_pictureID)].SetActive(true);
            textLine = textLine.Remove(textLine.IndexOf("+"), 2);
        }

        //Add sound
        if (textLine.Contains("*"))
        {
            string _soundID = textLine[textLine.IndexOf("*") + 1].ToString();
            audioSource.PlayOneShot(sounds[int.Parse(_soundID)]);
            textLine = textLine.Remove(textLine.IndexOf("*"), 2);
        }
        else
        {
            _text = textLine;
        }
    }

    void Type()
    {
        _timeType += Time.deltaTime;
        while (_timeType >= typeDelay && _typedCharacters.Length < _text.Length)
        {
            _typedCharacters += _text[_typedCharacters.Length];
            _timeType = 0;
        }
        _dialog.text = _typedCharacters;       
    }

    protected void ClearText()
    {
        _dialog.text = "";
        _typedCharacters = "";
        _text = "";
    }
}
