using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI levelup_text;
    public GameObject LevelUPEffect;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // 씬 전환 후에도 유지
    }
}
