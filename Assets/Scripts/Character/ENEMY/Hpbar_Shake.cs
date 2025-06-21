using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hpbar_Shake : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 originalPos;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPos = rectTransform.anchoredPosition;
    }

    public void ShakeHpBar(float shakeTime = 0.3f, float shakeIntensity = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(shakeTime, shakeIntensity));
    }

    private IEnumerator ShakeRoutine(float shakeTime, float shakeIntensity)
    {
        float elapsed = 0f;

        while (elapsed < shakeTime)
        {
            float x = 0;
            float y = Random.Range(-0.1f, 0.1f) * shakeIntensity;
            rectTransform.anchoredPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPos;
    }
}
