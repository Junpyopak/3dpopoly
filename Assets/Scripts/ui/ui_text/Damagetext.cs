using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Damagetext : MonoBehaviour
{
    public Text text;
    public float moveSpeed = 3f;
    public float lifeTime = 1f;

    private float elapsed = 0f;
    private RectTransform rect;
    private Color originalColor;

    private void Awake()
    {
        if (text == null)
            text = GetComponentInChildren<Text>();
        rect = text.GetComponent<RectTransform>();
        originalColor = text.color;
    }
    void OnEnable()
    {
        elapsed = 0f;
        text.color = originalColor;
    }

    void Update()
    {
        
        rect.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;

        elapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(originalColor.a, 0, elapsed / lifeTime);
        Color newColor = text.color;
        newColor.a = alpha;
        text.color = newColor;
        if (elapsed > lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetText(int damage)
    {
        text.text = damage.ToString();
    }
}
