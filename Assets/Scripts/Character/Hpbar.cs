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
        HPBar.maxValue = MaxHP; //slider�� MaxValue�� �츮�� ���ϴ� ü�� �ִ�ġ�� �ʱ�ȭ
        HPBar.value = currentHP; //slider�� value ���� ������ HP ������ �ʱ�ȭ
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
