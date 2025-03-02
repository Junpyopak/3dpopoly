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
    int MaxCnt = 3;
    private bool Failed = false;
    private bool Succes = false;
    [SerializeField]
    public Text fishText;
    // Start is called before the first frame update
    void Start()
    {
        Slider = GetComponent<Slider>();
        fishText.text = $"{((int)fishCnt)}/{MaxCnt}";
        Slider.value = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Fishing();
        if (fishCnt >= MaxCnt)
        {
            fishText.text = "성공!!";
            Succes = true;
            Slider.value = Slider.value;
            StartCoroutine(Failfish());
            fishCnt = 0;
        }
    }
    private void Fishing()
    {
        MinPos = 115f;
        MaxPos = 180f;
        if (Slider.value <= Slider.maxValue && Failed == false&&Succes==false)
        {
            Slider.value += Time.deltaTime * sliderSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Slider.value >= MinPos && Slider.value <= MaxPos)
                {
                    fishCnt++;
                    fishText.text = $"{((int)fishCnt)}/{MaxCnt}";
                    Debug.Log($"{fishCnt}");

                }
                else if (Slider.value != MinPos && Slider.value != MaxPos)
                {
                    Failed = true;
                    fishText.text = "<color=red>" + "실패..." + "</color>";
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
        yield return new WaitForSeconds(1.5f);
        Destroy(GameObject.Find("fishing"));
    }
}
