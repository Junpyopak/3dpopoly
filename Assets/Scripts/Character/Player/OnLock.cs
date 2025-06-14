using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLock : MonoBehaviour
{
    public float speed = 1f;       // 위아래 속도
    public float floatAmount = 0.2f; // 이동 거리

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * floatAmount;
        transform.localPosition = startPos + new Vector3(0, newY, 0);
    }
}
