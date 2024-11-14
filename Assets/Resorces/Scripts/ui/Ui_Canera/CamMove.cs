using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject Target;               // ī�޶� ����ٴ� Ÿ��

    public float offsetX = 0.0f;            // ī�޶��� x��ǥ
    public float offsetY = 1f;           // ī�޶��� y��ǥ
    public float offsetZ = -2.8f;          // ī�޶��� z��ǥ

    public float CameraSpeed = 10.0f;       // ī�޶��� �ӵ�
    Vector3 TargetPos;                      // Ÿ���� ��ġ

    public float angleX = 0.0f;
    public float angleY = 0.0f;
    public float Sensor = 100f;//���콺 ����

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
        // Ÿ���� x, y, z ��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ�� ����
        TargetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );

        // ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }
}
