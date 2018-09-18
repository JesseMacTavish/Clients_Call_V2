using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Button button = GetComponent<Button>();
        button.Select();
    }

    public void FinishDialog()
    {
        Destroy(transform.parent.gameObject);
    }
}
