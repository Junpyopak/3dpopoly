using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishTimer : MonoBehaviour
{
    private Slider Slider;
    private float sliderSpeed = 200f;
    public float MinPos;
    public float MaxPos;
    int fishCnt = 0;
    private bool Failed= false;
    // Start is called before the first frame update
    void Start()
    {
        Slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Fishing();
    }
    private void Fishing()
    {
        MinPos = 115f;
        MaxPos = 180f;
        if (Slider.value <= Slider.maxValue&&Failed==false)
        {
            Slider.value += Time.deltaTime * sliderSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Slider.value >= MinPos && Slider.value <= MaxPos)
                {
                    fishCnt++;
                    Debug.Log($"{fishCnt}");
                }
                else if(Slider.value != MinPos && Slider.value != MaxPos)
                {
                    Failed = true;
                    Slider.value = Slider.value;
                    StartCoroutine(Failfish());
                   // gameObject.SetActive(false);
                }
            }
            if (Slider.value == Slider.maxValue)
            {
                Slider.value = 0;
                return;
            }
        }    
    }
    IEnumerator Failfish()
    {
        yield return new WaitForSeconds(1.5f); ;
        gameObject.SetActive(false);
    }
}
