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

    private IEnumerator cortine;
    // Start is called before the first frame update
    void Start()
    {
        
        //for (int i = 0; i < StopCount; i++)
        //{
        //    int random = Random.Range(0, 100);
        //    Debug.Log($"{random}");
        //    if ((int)slider.value == random)
        //    {
        //        Time.timeScale = 0;
        //    }
        //}      
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        if (SceneLoadTime >= 13)
        {
            Pause();
            cortine = waitTime(5);
            StartCoroutine(cortine);
            if (SceneLoadTime >= LoadingTime)
            {
                SceneManager.LoadScene("LOBBY");
            }
            //if (SceneLoadTime >= 20)
            //{
            //    StopCoroutine(cortine);
            //    StartCoroutine (cortine);
            //    if (SceneLoadTime >= 60)
            //    {
            //        Pause();

            //        //if (SceneLoadTime >= LoadingTime)
            //        //{
            //        //    SceneManager.LoadScene("LOBBY");
            //        //}
            //    }
            //}
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

        SceneLoadTime += Time.deltaTime * 8f;
        initSlider();
        //if (SceneLoadTime >= 3)
        //{
        //    Pause();
        //    //StartCoroutine(waitTime(5));
        //    if (SceneLoadTime >= 63)
        //    {
        //        Pause();
        //        if (SceneLoadTime >= 78)
        //        {
        //            Pause();
        //            if (SceneLoadTime >= LoadingTime)
        //            {
        //                SceneManager.LoadScene("LOBBY");
        //            }
        //        }
        //    }

        //}


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
