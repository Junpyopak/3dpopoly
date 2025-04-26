using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChinCamShake : MonoBehaviour
{
    [Header("기본 설정")]
    public Transform Target;
    public float followSpeed = 10f;
    public float mouseSensitivity = 100f;
    public float clampAngle = 80f;

    [Header("줌 설정")]
    public float minDistance = 2f; // 가까운 줌
    public float maxDistance = 5f; // 기본 거리
    public float zoomSpeed = 5f;

    [Header("카메라 흔들림 설정")]
    public bool isShaking = false;
    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.5f;
    private float shakeTimer = 0f;

    private float rotX;
    private float rotY;

    private CinemachineVirtualCamera virtualCam;
    private Transform camTransform;

    private Vector3 originalLocalPosition;
    private float currentDistance;

    private Player player;

    void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        if (virtualCam == null)
        {
            Debug.LogError("CinemachineVirtualCamera 컴포넌트가 없습니다!");
            enabled = false;
            return;
        }

        camTransform = transform;
        Vector3 rot = camTransform.rotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;

        currentDistance = maxDistance;
        originalLocalPosition = camTransform.localPosition;

        GameObject playerObj = GameObject.Find("character");
        if (playerObj != null)
            player = playerObj.GetComponent<Player>();
    }

    void Update()
    {
        if (Target == null) return;

        MouseControl();
        HandleZoom();
        HandleShake();
    }

    void LateUpdate()
    {
        if (Target == null) return;

        // 플레이어 따라가기
        camTransform.position = Vector3.Lerp(camTransform.position, Target.position, followSpeed * Time.deltaTime);
    }

    void MouseControl()
    {
        if (player != null && player.AutoMode) return; // 자동모드에서는 고정

        rotX += -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0f);
        camTransform.rotation = localRotation;
    }

    void HandleZoom()
    {
        if (Input.GetKey(KeyCode.Z))
            currentDistance = Mathf.Lerp(currentDistance, minDistance, Time.deltaTime * zoomSpeed);
        else
            currentDistance = Mathf.Lerp(currentDistance, maxDistance, Time.deltaTime * zoomSpeed);

        Vector3 zoomDir = camTransform.forward * -currentDistance;
        virtualCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = zoomDir;
    }

    void HandleShake()
    {
        if (isShaking)
        {
            if (shakeTimer > 0)
            {
                Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;
                camTransform.localPosition = originalLocalPosition + shakeOffset;

                shakeTimer -= Time.deltaTime;
            }
            else
            {
                isShaking = false;
                shakeTimer = 0f;
                camTransform.localPosition = originalLocalPosition;
            }
        }
    }

    public void StartShake(float duration, float amount)
    {
        shakeDuration = duration;
        shakeAmount = amount;
        shakeTimer = duration;
        isShaking = true;
    }
}

