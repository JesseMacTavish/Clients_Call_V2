using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogHandler : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject optionsBox;
    public GameObject sliders;
    public AudioSource audioSource;
    [Space]
    [Range(2, 3)]
    public int ammountOfOptions;
    public bool timedDecision;
    public float givenTime;
    public bool talkToOtherCharacter;
    public GameObject otherCharacter;

    float _timeOnDecision;
    Text _dialog;
    Text _name;
    string[] _fullTextOptions;
    int _dialogBoxID;
    bool _optionsOn;
    bool _questionAsked;

    [Space]
    [TextArea]
    public List<string> dialogScript = new List<string>();
    [TextArea]
    public List<string> answersScript = new List<string>();
    public List<GameObject> pictures = new List<GameObject>();
    public List<AudioClip> sounds = new List<AudioClip>();

    List<Action> optionsAction = new List<Action>();

    //Variables for slow type
    public float typeDelay = 0.1f;
    float _timeType;
    string _text = "";
    string _typedCharacters = "";

    [Header("Scenes to load")]
    public UnityEngine.Object sceneToLoad1;
    public UnityEngine.Object sceneToLoad2;
    public UnityEngine.Object sceneToLoad3;

    public SetBool Op1;
    public SetBool Op2;
    public SetBool Op3;

    private bool _op1;
    private bool _op2;
    private bool _op3;

    void Start()
    {
        _dialog = dialogBox.GetComponentInChildren<Text>();
        _name = dialogBox.transform.Find("Name").GetComponent<Text>();
        string[] script = dialogScript[0].Split('|');
        _name.text = script[0];
        _text = script[1];

        optionsAction.Add(Option1);
        optionsAction.Add(Option2);
        optionsAction.Add(Option3);
    }

    void Update()
    {
        Type();

        if (Input.GetButtonDown("Fire1") && !_optionsOn)
        {
            Continue();
        }

        if (_optionsOn && timedDecision)
        {
            CountDown();
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

        if (_typedCharacters.Length == _text.Length && _questionAsked)
        {
            ShowOptions();
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
            if (!talkToOtherCharacter)
            {
                dialogBox.SetActive(false);
                if (_op1)
                {
                    SceneManager.LoadScene(sceneToLoad1.name);
                }
                else if (_op2)
                {
                    SceneManager.LoadScene(sceneToLoad2.name);
                }

                else if (_op3)
                {
                    SceneManager.LoadScene(sceneToLoad3.name);
                }
            }
            else
            {
                otherCharacter.SetActive(true);
                gameObject.SetActive(false);
            }
            return;
        }


        string[] _script = dialogScript[_dialogBoxID].Split('|');
        _name.text = _script[0];
        string textLine = _script[1];

        //Add picture
        if (textLine.Contains("+"))
        {
            string _pictureID = textLine[textLine.IndexOf("+") + 1].ToString();
            pictures[Int32.Parse(_pictureID)].SetActive(true);
            textLine = textLine.Remove(textLine.IndexOf("+"), 2);
        }

        //Add sound
        if (textLine.Contains("*"))
        {
            string _soundID = textLine[textLine.IndexOf("*") + 1].ToString();
            audioSource.PlayOneShot(sounds[Int32.Parse(_soundID)]);
            textLine = textLine.Remove(textLine.IndexOf("*"), 2);
        }

        //Add quesion
        if (textLine.Contains("%"))
        {
            _fullTextOptions = textLine.Split('%');
            _text = _fullTextOptions[0];
            _questionAsked = true;
        }
        else
        {
            _text = textLine;
        }
    }

    void ShowOptions()
    {
        for (int i = 0; i < ammountOfOptions; i++)
        {
            GameObject option = optionsBox.transform.GetChild(i).gameObject;
            option.SetActive(true);
            option.GetComponentInChildren<Text>().text = _fullTextOptions[i + 1];
            int x = i;
            option.GetComponent<Button>().onClick.AddListener(() => optionsAction[x]());
        }//Need to select one of the options so u can interact with them with arrows
        optionsBox.transform.GetChild(0).GetComponent<Button>().Select();

        optionsBox.SetActive(true);
        if (timedDecision)
        {
            sliders.SetActive(true);
            SetTimeSlider(1f);
            _timeOnDecision = 0f;
        }

        _optionsOn = true;
        _questionAsked = false;        
    }

    protected void CloseOptions()
    {
        for (int i = 0; i < ammountOfOptions; i++)
        {
            GameObject option = optionsBox.transform.GetChild(i).gameObject;
            option.SetActive(false);
        }

        _optionsOn = false;
        optionsBox.SetActive(false);
        sliders.SetActive(false);
    }

    protected void ClearText()
    {
        _dialog.text = "";
        _typedCharacters = "";
        _text = "";
    }

    void CountDown()
    {
        _timeOnDecision += Time.deltaTime;
        SetTimeSlider(1f - _timeOnDecision / givenTime);
        if (_timeOnDecision > givenTime)
        {
            optionsAction[UnityEngine.Random.Range(0, optionsAction.Count)]();
        }
    }

    void SetTimeSlider(float pValue)
    {
        sliders.transform.Find("Slider 1").GetComponent<Slider>().value = pValue;
        sliders.transform.Find("Slider 2").GetComponent<Slider>().value = pValue;
    }

    public void Option1()
    {
        DecisionTracker.ToggleBool(Op1);

        dialogScript = answersScript[0].Split('^').ToList();
        _dialogBoxID = -1;
        ClearText();

        _op1 = true;
        Continue();
        CloseOptions();
    }

    public void Option2()
    {
        DecisionTracker.ToggleBool(Op2);

        dialogScript = answersScript[1].Split('^').ToList();
        _dialogBoxID = -1;
        ClearText();

        _op2 = true;
        Continue();
        CloseOptions();
    }

    public void Option3()
    {
        DecisionTracker.ToggleBool(Op3);

        dialogScript = answersScript[2].Split('^').ToList();
        _dialogBoxID = -1;
        ClearText();

        _op3 = true;
        Continue();
        CloseOptions();
    }
}
