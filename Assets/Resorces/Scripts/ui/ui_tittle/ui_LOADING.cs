using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ui_LOADING : MonoBehaviour
{
    [Header("·ÎµùÃ¢")]
    public Slider slider;
    public Image sliderFill;
    public Text textTimer;
    public float SceneLoadTime = 0;
    public float LoadingTime = 100f;
    public int StopCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < StopCount; i++)
        {
            int random = Random.Range(0, 100);
            Debug.Log($"{random}");
            if ((int)slider.value == random)
            {
                Time.timeScale = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer();

    }

    private void initSlider()
    {
        slider.maxValue = LoadingTime;
        slider.value = SceneLoadTime;
        textTimer.text = $"{((int)slider.value).ToString("D2")} % ";
    }
    private void Timer()
    {

        SceneLoadTime += Time.deltaTime *2f;
        initSlider();
        if (SceneLoadTime >= LoadingTime)
        {
            SceneManager.LoadScene("LOBBY");
        }


    }
}
