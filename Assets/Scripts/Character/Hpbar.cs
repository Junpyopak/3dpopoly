using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hpbar : MonoBehaviour
{
    public float MaxHP = 100;
    public float currentHP =100;
    public Slider HPBar;
    public Image sliderFill;

    void Start()
    {
        HPBar = GameObject.Find("Slider").GetComponent<Slider>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentHP -= 0.5f;
            HpCheck();
        }
        
    }

    void HpCheck()
    {
        HPBar.value = currentHP;
        HPBar.maxValue = MaxHP;
    }
}
