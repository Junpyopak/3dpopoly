using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float moveSpeed = 3f;       // 이동 속도
    public float maxDistance = 5f;

    private Vector3 startPos;
    private bool isMoving = true;
    private ParticleSystem ps;

    void Start()
    {
        startPos = transform.position;
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position -= transform.up * moveSpeed * Time.deltaTime;

            float traveled = Vector3.Distance(startPos, transform.position);
            if (traveled >= maxDistance)
            {
                isMoving = false;
                Invoke("StopAndDestroy", 3f);
            }
        }

    }

    void StopAndDestroy()
    {
        if (ps != null)
        {
            ps.Stop();
        }
        Destroy(gameObject, 1f);
    }

}
