using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ui_BossLoad : MonoBehaviour
{
    [Header("�ε�â")]
    public Slider slider;
    public Image sliderFill;
    public Text textTimer;
    public float SceneLoadTime = 0;
    public float LoadingTime = 100f;
    public int StopCount = 5;

    private IEnumerator cortine;
    // Start is called before the first frame update
    void Start()
    {     
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        if (SceneLoadTime >= 46)
        {
            Pause();
            cortine = waitTime(5);
            StartCoroutine(cortine);
            if (SceneLoadTime >= LoadingTime)
            {
                SceneManager.LoadScene("BossLOBBY");
            }
        }
    }
    public IEnumerator waitTime(float wait)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(wait);
            Resume();
        }
    }
    private void initSlider()
    {
        slider.maxValue = LoadingTime;
        slider.value = SceneLoadTime;
        textTimer.text = $"{((int)slider.value).ToString("D2")} % ";
    }
    private void Timer()
    {

        SceneLoadTime += Time.deltaTime * 10f;
        initSlider();
    }
    private void Pause()
    {
        Time.timeScale = 0;
    }
    void Resume()
    {
        Time.timeScale = 1;
    }
}
