using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TittleCamMove : MonoBehaviour
{
    public Transform targetPosition;
    public float MoveSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition.position, MoveSpeed * Time.deltaTime);
    }
}
