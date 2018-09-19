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

        if (DecisionTracker.sacrificedEdwin)
        {
            dialogScript.Add("Edwin chose to sacrifice himself.");
            dialogScript.Add("He lived the rest of his life as a servant to serve the Lord.");
            dialogScript.Add("He found happiness in the thought of doing this for his daughter, and did not regret his sacrifice.");
        }

        else if (DecisionTracker.killedLord && DecisionTracker.acceptedWilliam)
        {
            dialogScript.Add("Edwin chose to kill the Lord, and was never caught. However, his condition got worse and he was afraid he would eventually snap and kill someone.");
            dialogScript.Add("He could no longer part reality from his visions.");
            dialogScript.Add("He was therefore unable to work with William, and stayed poor.");
            dialogScript.Add("Later on he was locked up for being unpredictable and dangerous.");
        }

        else if (DecisionTracker.killedLord && DecisionTracker.rejectedWilliam)
        {
            dialogScript.Add("Edwin chose to kill the Lord, and was never caught. However, his condition got worse and he was afraid he would eventually snap and kill someone.");
            dialogScript.Add("He could no longer part reality from his visions.");
            dialogScript.Add("Later on he was locked up for being unpredictable and dangerous.");
        }

        else if (DecisionTracker.sacrificedDaughter && DecisionTracker.savedOscar && DecisionTracker.acceptedWilliam)
        {
            dialogScript.Add("Edwin decided to kill Oscar to sacrifice Karoline and save Emily.");
            dialogScript.Add("He partnered up with William, and despite disliking the horrifying practices, he kept it up to be able to give Emily a better life.");
            dialogScript.Add("He would never reveal what he did for a living. Whenever someone asked, Edwin would say 'I work for the greater good'. ");
        }

        else if (DecisionTracker.sacrificedDaughter && DecisionTracker.savedOscar && DecisionTracker.rejectedWilliam)
        {
            dialogScript.Add("Edwin decided to kill Oscar to sacrifice Karoline and save Emily.");
            dialogScript.Add("He returned home with Emily, still with no money and no future.");
        }


        else if (DecisionTracker.sacrificedDaughter && DecisionTracker.killedOscar && DecisionTracker.acceptedWilliam)
        {
            dialogScript.Add("Edwin decided to sacrifice Oscar’s daughter, Karoline, to save Emily.");
            dialogScript.Add("He partnered up with William, and despite disliking the horrifying practices, he kept it up to be able to give Emily a better life.");
            dialogScript.Add("He would never reveal what he did for a living. Whenever Emily asked, he would say 'I work for the greater good'.");
        }


        else if (DecisionTracker.sacrificedDaughter && DecisionTracker.killedOscar && DecisionTracker.rejectedWilliam)
        {
            dialogScript.Add("Edwin decided to sacrifice Oscar’s daughter, Karoline, to save Emily.");
            dialogScript.Add("He returned home with Emily, still with no money and no hope for the future.");
        }


        //__________________________________________________________________________________________________________________________________________

        if (DecisionTracker.savedAngela && DecisionTracker.killedBoth)
        {
            dialogScript.Add("Angela was never caught. She moved to a hidden lodge deep in the forest where she continued her nefarious experiments. She gained some wealth.");
        }
       else if (DecisionTracker.savedAngela && DecisionTracker.eKilledwAlive)
        {
            dialogScript.Add("Angela was never caught. She moved to a hidden lodge deep in the forest where she continued her nefarious experiments. She gained great wealth and became the witch master.");
        }

        else if (DecisionTracker.savedAngela && DecisionTracker.wKilledeAlive)
        {
            dialogScript.Add("Angela was never caught. She moved to a hidden lodge deep in the forest where she continued her nefarious experiments. She gained great wealth and became the witch master.");
        }

        else if (DecisionTracker.killedAngela)
        {
            dialogScript.Add("Angela was deemed guilty for witchery and attempted murder in several occasions, and was sentenced for life. ");
        }

        //__________________________________________________________________________________________________________________________________________

        if (DecisionTracker.savedOscar && DecisionTracker.sacrificedDaughter)
        {
            dialogScript.Add("Oscar was disappointed with Edwin’s sacrifice.");
            dialogScript.Add(" Deep down he knew he would have done the same to save Karoline, but that feeling was not enough to forgive Edwin. Their friendship was forever ruined.");
        }

        else if (DecisionTracker.savedOscar && DecisionTracker.sacrificedEdwin)
        {
            dialogScript.Add("Oscar respected Edwin more, but he was upset they would never meet in private again.");
            dialogScript.Add("He still came to visit Edwin regularly. Oscar lived happily with his daughter for many years.");
        }

        else if (DecisionTracker.savedOscar && DecisionTracker.killedLord)
        {
            dialogScript.Add("Oscar found it suspicious how Emily was released right after the Lord was mysteriously killed, but he never questioned anything.");
            dialogScript.Add("He was happy that Edwin got his Emily back, and their friendship was maintained. Oscar lived happily with Karoline for many years.");
        }

        else if (DecisionTracker.killedOscar && DecisionTracker.sacrificedDaughter)
        {
            dialogScript.Add("Oscar was released a few years later, but his friendship with Edwin was forever ruined.");
            dialogScript.Add("Deep down he knew he would have done the same thing, but that feeling was not enough to forgive Edwin for sacrificing Karoline.");
        }

        else if (DecisionTracker.killedOscar && DecisionTracker.sacrificedEdwin)
        {
            dialogScript.Add("Oscar was released a few years later.");
            dialogScript.Add("He respected Edwin more thereafter, but he was upset they would never get to see each other  in private.");
            dialogScript.Add("He would regularly come to visit. He lived happily with Karoline for many years.");
        }

        else if (DecisionTracker.killedOscar && DecisionTracker.killedLord)
        {
            dialogScript.Add("Oscar was released a few years later.");
            dialogScript.Add("He did find it suspicious how Emily was released right after the Lord was mysteriously killed, but he never questioned anything.");
            dialogScript.Add("He was happy that Edwin got his Emily back, and their friendship was maintained. Oscar lived happily with Karoline for many years.");
        }

        //__________________________________________________________________________________________________________________________________________

        if (DecisionTracker.eKilledwAlive && DecisionTracker.acceptedWilliam && DecisionTracker.sacrificedDaughter)
        {
            dialogScript.Add("William was not caught for using his unconventional methods.");
            dialogScript.Add("He partnered up with Edwin. Edwin did not enjoy it, but he did his best to keep his job.");
        }

        else if (DecisionTracker.eKilledwAlive && DecisionTracker.acceptedWilliam && DecisionTracker.killedLord)
        {
            dialogScript.Add("William was not caught for using his unconventional methods, but he struggled finding a new partner.");
            dialogScript.Add("After firing a bunch of apprentices, he found the perfect partner with the same visions and opinions as himself.");
        }

        else if (DecisionTracker.eKilledwAlive && DecisionTracker.acceptedWilliam && DecisionTracker.sacrificedEdwin)
        {
            dialogScript.Add("William was not caught for using his unconventional methods, but he struggled finding a new partner.");
            dialogScript.Add("After firing a bunch of apprentices, he found the perfect partner with the same visions and opinions as himself.");
            dialogScript.Add("After a few years, however, his partner took William’s life to continue his business on his own.");
        }

        else if (DecisionTracker.eKilledwAlive && DecisionTracker.rejectedWilliam)
        {
            dialogScript.Add("William was investigated and got judged as guilty shortly after. He got sentenced for life.");

        }

        else if (DecisionTracker.saveBoth)
        {
            dialogScript.Add("William kept doing his experiments assisted by Elizabeth.");

        }

        else if (DecisionTracker.wKilledeAlive)
        {
            dialogScript.Add("William was brutally murdered by Angela.");
        }

        else if (DecisionTracker.killedBoth && DecisionTracker.savedAngela)
        {
            dialogScript.Add("William and Elizabeth got brutally murdered by Angela.");
 
        }

        else if (DecisionTracker.killedBoth && DecisionTracker.killedAngela)
        {
            dialogScript.Add("William and Elizabeth were investigated and were judged as guilty shortly after. They both got sentenced for life.");
        }

        //__________________________________________________________________________________________________________________________________________

        if (DecisionTracker.wKilledeAlive)
        {
            dialogScript.Add("Elizabeth never got caught for helping her father, and she never returned to his business.");
            dialogScript.Add("She occasionally missed him, but overall she was happy to be free from his madness.");
        }

        else if (DecisionTracker.saveBoth)
        {
            dialogScript.Add("Elizabeth assisted her dad for a few months, until she finally had enough.");
            dialogScript.Add("She turned him in for torture and murder, and he got sentenced for life.");
            dialogScript.Add("She occasionally visited him to see how he was doing, but she was happy to be free from his madness.");
        }

        else if (DecisionTracker.eKilledwAlive && DecisionTracker.savedAngela)
        {
            dialogScript.Add("Elizabeth got brutally murdered by angela.");
        }

        //        if (DecisionTracker.killedBoth && DecisionTracker.killedAngela)
        //        {
        //            dialogScript.Add("");
        //        }


        //__________________________________________________________________________________________________________________________________________



        if (DecisionTracker.killedLord)
        {
            dialogScript.Add("Emily was disappointed with her father’s decisions, but she managed to forgive him eventually.");
            dialogScript.Add("As Edwin’s conditions got worse, Emily started to get scared of him and ran away from home.");
            dialogScript.Add("She lived her life alone, but after many difficult years she managed to work her way to becoming a successful artist and get off the streets.");
        }

        else if (DecisionTracker.sacrificedEdwin)
        {
            dialogScript.Add("Emily admires her father for his sacrifice.");
            dialogScript.Add("With Edwin locked up, however, she had to live on her own.");
            dialogScript.Add("After many difficult years, she finally managed to work her way up to becoming a successful artist, and lived a good life off the streets.");
        }


        else if (DecisionTracker.sacrificedDaughter && DecisionTracker.savedOscar && DecisionTracker.acceptedWilliam)
        {
            dialogScript.Add("Emily was disgusted with her father’s actions, and soon after she was released she decided to run away from home to live on her own.");
            dialogScript.Add("After many difficult years, she finally managed to work her way up to becoming a successful artist, and lived a good life off the streets.");
            dialogScript.Add("She never talked to her dad again.");
        }

        else if (DecisionTracker.sacrificedDaughter && DecisionTracker.savedOscar && DecisionTracker.rejectedWilliam)
        {
            dialogScript.Add("Emily was disgusted with her father’s actions, and soon after she was released she decided to run away from home to live on her own.");
            dialogScript.Add("After many difficult years, she finally managed to work her way up to becoming a successful artist, and lived a good life off the streets.");
            dialogScript.Add("She never talked to her dad again.");
        }

        else if (DecisionTracker.sacrificedDaughter && DecisionTracker.killedOscar && DecisionTracker.acceptedWilliam)
        {
            dialogScript.Add("Emily disagreed with Edwin’s decisions, and refused to talk to him at first.");
            dialogScript.Add("She could not forgive him for a few years, but she eventually did.");
            dialogScript.Add("She found it strange how the father would not say what his new job was, but figured it would be better if she didn’t know.");
            dialogScript.Add("Emily lived a good and rich life, and grew up to become a successful artist.");
        }

        else if (DecisionTracker.sacrificedDaughter && DecisionTracker.killedOscar && DecisionTracker.rejectedWilliam)
        {
            dialogScript.Add("Emily disagreed with Edwin’s decisions, and refused to talk to him at first.");
            dialogScript.Add("She could not forgive him for a few years, but eventually did.");
            dialogScript.Add("They lived a poor life, but they were happy to be together again.");
        }

        //__________________________________________________________________________________________________________________________________________

        if (DecisionTracker.killedAngela && DecisionTracker.killedOscar && DecisionTracker.killedBoth)
        {
            dialogScript.Add("Edwin brought Emily home shortly after. He was happy to have her back, but wished he could have secured a better future for her.");
            dialogScript.Add("Emily was happy with her dad for fighting for her freedom. She lived a life in poverty, but she was happy to be out.");
            dialogScript.Add("After many difficult years, she finally managed to work her way up to becoming a successful artist, and lived a good life off the streets.");
        }

        //__________________________________________________________________________________________________________________________________________

        if (DecisionTracker.killedLord)
        {
            dialogScript.Add("The town did not go to war, but struggling to find a worthy leader, crime rates raised, poverty rates increased, and all of society turned to chaos.");
        }

        //__________________________________________________________________________________________________________________________________________



        dialogScript.Add("I wonder how it would have ended if things happened differently...");
       

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
