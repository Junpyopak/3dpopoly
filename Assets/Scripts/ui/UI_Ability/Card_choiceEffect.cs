using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card_choiceEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Vector3 originalPosition;
    private Vector3 originalScale;

    public float hoverHeight = 40f;
    public float scaleUp = 1.1f;
    public float moveSpeed = 10f;

    private bool isHovered = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = isHovered
            ? originalPosition + Vector3.up * hoverHeight
            : originalPosition;

        Vector3 targetScale = isHovered
            ? originalScale * scaleUp
            : originalScale;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * moveSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
