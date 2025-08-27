using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Clear : MonoBehaviour
{
    public static Ui_Clear Instance { get; private set; }

    [SerializeField] private GameObject levelClearPanel;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite filledStar;

    private Coroutine starCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        levelClearPanel.SetActive(false);
    }

    public void ShowLevelClear(int starCount)
    {
        levelClearPanel.SetActive(true);
        ResetStars();
        starCoroutine = StartCoroutine(AnimateStars(starCount));
    }

    private void ResetStars()
    {
        if (starCoroutine != null) StopCoroutine(starCoroutine);
        foreach (var star in stars)
            star.sprite = emptyStar;
    }

    private IEnumerator AnimateStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            yield return new WaitForSeconds(0.3f); // 순차적으로 채우기
            stars[i].sprite = filledStar;
        }
    }
}
