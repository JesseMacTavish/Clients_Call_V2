using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Slider _healthBar;

    private void Start()
    {
        _healthBar = GetComponent<Slider>();
    }

    public void UpdateHealthBar(float pValue)
    {
        _healthBar.value = pValue;
    }
}
