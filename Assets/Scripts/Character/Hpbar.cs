using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hpbar : MonoBehaviour
{
    public float MaxHP;
    public float currentHP;
    public Slider HPBar;

    void Start()
    {
        currentHP = MaxHP;
        HPBar.maxValue = MaxHP; //slider의 MaxValue를 우리가 원하는 체력 최대치로 초기화
        HPBar.value = currentHP; //slider의 value 값을 현재의 HP 값으로 초기화
    }

    void Update()
    {
        Damage();
        HPBar.value = currentHP;
    }

    void Damage()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            currentHP -= 10;
        }       
    }
}
