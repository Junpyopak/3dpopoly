using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float moveSpeed = 3f;       // 이동 속도
    public float maxDistance = 3f;

    private Vector3 startPos;
    private bool isMoving = false;
    private bool isReturning = false;
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        startPos = transform.position;
    }

    void Update()
    {
        // 파티클이 플레이되면 이동 시작
        if (!isMoving && ps != null && ps.isPlaying && !isReturning)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position -= transform.up * moveSpeed * Time.deltaTime;

            float traveled = Vector3.Distance(startPos, transform.position);
            if (traveled >= maxDistance)
            {
                isMoving = false;
                Invoke("StopAndReturn", 3f);  // 3초 후 복귀 시작
            }
        }

        if (isReturning)
        {
            // 복귀 이동 (lerp로 부드럽게 이동)
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, startPos) < 0.01f)
            {
                isReturning = false;
            }
        }
    }

    void StopAndReturn()
    {
        if (ps != null)
        {
            ps.Stop();
        }
        isReturning = true;
    }

}
