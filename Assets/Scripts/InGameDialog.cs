using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InGameDialog : MonoBehaviour
{
    public GameObject dialogBox;
    public AudioSource audioSource;
    private GameObject _player;

    [Space]
    [TextArea]
    public List<string> dialogScript = new List<string>();
    public List<AudioClip> sounds = new List<AudioClip>();

    Text _dialog;
    Text _name;
    int _dialogBoxID = -1;
    //Variables for slow type
    public float typeDelay = 0.03f;
    float _timeType;
    string _text = "";
    string _typedCharacters = "";


    void Start()
    {
        _dialog = dialogBox.GetComponentInChildren<Text>();
        _name = dialogBox.transform.Find("Name").GetComponent<Text>();
        _player = GameObject.FindGameObjectWithTag("Player");

        Continue();
    }

    void Update()
    {
        Type();

        if (Input.GetButtonDown("Fire1") && _dialog.IsActive())
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
            dialogBox.SetActive(false);
            Destroy(gameObject);

            _player.GetComponent<Movement>().enabled = true;
            _player.GetComponent<Attack>().enabled = true;
        }

        string[] _script = dialogScript[_dialogBoxID].Split('|');
        _name.text = _script[0];
        string textLine = _script[1];

        //Add sound
        if (textLine.Contains("*"))
        {
            string _soundID = textLine[textLine.IndexOf("*") + 1].ToString();
            audioSource.PlayOneShot(sounds[Int32.Parse(_soundID)]);
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

    void OnTriggerEnter(Collider other)
    {
        if (dialogBox != null)
            dialogBox.SetActive(true);

        _player.GetComponent<Movement>().enabled = false;
        _player.GetComponent<Attack>().enabled = false;
        _player.GetComponent<PlayerAnimation>().StopWalking();
    }   
}
