using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopNovelMusic : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("BGMusic") != null)
            Destroy(GameObject.FindGameObjectWithTag("BGMusic"));
    }
}
