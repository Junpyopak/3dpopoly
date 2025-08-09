using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFade : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Button button; //버튼 클릭감지
    [SerializeField] TMP_Text countText;
    [SerializeField] float twinkleTime = 2f;//알파값 반짝임
    [SerializeField] private GameObject avillity;

    bool isFill = false;
    bool isBlinking = false;
    int count = 0;
    void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        if (button == null)
            button = GetComponent<Button>();

        if (button != null)
            button.onClick.AddListener(OnButtonClicked);

        if (countText != null)
            countText.text = "0";

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isBlinking) return;

        if (isFill && canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha -= Time.deltaTime / twinkleTime;
            if (canvasGroup.alpha <= 0) isFill = false;
        }
        else if (!isFill && canvasGroup.alpha <= 1)
        {
            canvasGroup.alpha += Time.deltaTime / twinkleTime;
            if (canvasGroup.alpha >= 1) isFill = true;
        }
    }
    //플레이어 레벨업
    public void ShowOnLevelUp()
    {
        count++;
        if (countText != null)
            countText.text = count.ToString();

        gameObject.SetActive(true);
        isBlinking = true;
        isFill = false;
        canvasGroup.alpha = 1;
    }

    void OnButtonClicked()
    {
        count--;
        avillity?.SetActive(true);
        if (count < 0) count = 0;

        if (countText != null)
            countText.text = count.ToString();

        if (count <= 0)
        {
            isBlinking = false;
            gameObject.SetActive(false);
        }
    }
}
