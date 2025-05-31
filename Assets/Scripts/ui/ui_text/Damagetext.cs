using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Damagetext : MonoBehaviour
{
    public Text text;
    public float moveSpeed = 1f;
    public float lifeTime = 1f;

    private float elapsed = 0f;
    private Transform cam;

    private void Awake()
    {
        if (text == null)
            text = GetComponentInChildren<Text>();
    }
    void OnEnable()
    {
        elapsed = 0f;
        cam = Camera.main.transform;
    }

    void Update()
    {
        // ���� �̵�
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // �׻� ī�޶� ��������
        transform.forward = cam.forward;

        elapsed += Time.deltaTime;
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
