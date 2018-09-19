using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionScreen : MonoBehaviour
{
    public Sprite logo1;
    public Sprite logo2;
    public Sprite logo3;
    public Sprite logo4;
    float highestValue;
    
    private List<float> types = new List<float>();

    void Start()
    {
        types.Add(DecisionTracker.killer);
        types.Add(DecisionTracker.explorer);
        types.Add(DecisionTracker.achiever);
        types.Add(DecisionTracker.socializer);

        highestValue = Mathf.Max(DecisionTracker.killer, DecisionTracker.explorer, DecisionTracker.achiever, DecisionTracker.socializer);

        if (highestValue == DecisionTracker.killer)
        {
            transform.Find("Image").GetComponent<Image>().sprite = logo1;
            transform.Find("Text 2").GetComponent<Text>().text += " Killer";
        }
        else if(highestValue == DecisionTracker.explorer)
        {
            transform.Find("Image").GetComponent<Image>().sprite = logo2;
            transform.Find("Text 2").GetComponent<Text>().text += " Explorer";
        }
        else if (highestValue == DecisionTracker.achiever)
        {
            transform.Find("Image").GetComponent<Image>().sprite = logo3;
            transform.Find("Text 2").GetComponent<Text>().text += " Achiever";
        }
        else if (highestValue == DecisionTracker.socializer)
        {
            transform.Find("Image").GetComponent<Image>().sprite = logo4;
            transform.Find("Text 2").GetComponent<Text>().text += " Socializer";
        }        
    }
}
