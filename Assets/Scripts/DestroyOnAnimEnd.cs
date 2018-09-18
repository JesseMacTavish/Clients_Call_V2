using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimEnd : MonoBehaviour
{
    void Destroy()
    {
        Destroy(gameObject);
    }
}
