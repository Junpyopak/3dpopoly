using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hpbar : MonoBehaviour
{
    public float MaxHP = 100;
    public float currentHP =0;
    public Slider HPBar;

    void Start()
    {
        HPBar.value = (float)currentHP / (float)MaxHP;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentHP -= 1f;
        }
        HpCheck();
    }

    void HpCheck()
    {
        HPBar.value = currentHP;
    }
}
