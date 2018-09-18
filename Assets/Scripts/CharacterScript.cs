using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public GameObject dialog;

    void OnTriggerEnter(Collider other)
    {
        if (dialog != null)
            dialog.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (dialog != null)
            dialog.SetActive(false);
    }

    public void KillSanta()
    {
        Destroy(gameObject);
        Destroy(dialog);
    }

}
