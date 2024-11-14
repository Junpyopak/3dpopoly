using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject Target;               // 카메라가 따라다닐 타겟

    public float offsetX = 0.0f;            // 카메라의 x좌표
    public float offsetY = 1f;           // 카메라의 y좌표
    public float offsetZ = -2.8f;          // 카메라의 z좌표

    public float CameraSpeed = 10.0f;       // 카메라의 속도
    Vector3 TargetPos;                      // 타겟의 위치

    public float angleX = 0.0f;
    public float angleY = 0.0f;
    public float Sensor = 100f;//마우스 감도

    void Start()
    {
        angleX = transform.localRotation.eulerAngles.x;
        angleY = transform.localRotation.eulerAngles.y;
    }
    void Update()
    {
        angleX += -(Input.GetAxis("Mouse Y"))*Sensor*Time.deltaTime;
        angleY += Input.GetAxis("Mouse X") * Sensor * Time.deltaTime;
        Quaternion root = Quaternion.Euler(angleX, angleY, 0);
        transform.rotation = root;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 타겟의 x, y, z 좌표에 카메라의 좌표를 더하여 카메라의 위치를 결정
        TargetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );

        // 카메라의 움직임을 부드럽게 하는 함수(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }
}
