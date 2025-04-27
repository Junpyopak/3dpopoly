using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowUi : MonoBehaviour
{
    public Transform Target;
    public float FollowSpeed = 20f;
    public float MouseSensor = 100f;//마우스 감도
    public float LimitAngle = 70f;//제한각도

    private float rotaX;
    private float rotaY;

    public Transform Camera;
    public Vector3 dir;
    public Vector3 finalDir;//마지막거리

    public float minDis = 0.1f;//최소
    public float maxDis = 2f;//최대
    public float finalDis;
    public float Smove = 10f;
    public float ZoomSmoothSpeed = 5f;

    private float currentDis;
    public bool OnShake { set; get; }
    private Player player;

    void Start()
    {
        rotaX = transform.localRotation.eulerAngles.x;
        rotaY = transform.localRotation.eulerAngles.y;

        if (Camera == null)
        {
            Debug.LogError(" Camera 트랜스폼이 연결되지 않았습니다!");
            return;
        }

        dir = Camera.localPosition.normalized;
        if (dir.magnitude == 0)
        {
            Debug.LogWarning(" dir 값이 0입니다. Camera.localPosition을 확인해주세요!");
        }

        finalDis = Camera.localPosition.magnitude;
        currentDis = finalDis;

        GameObject playerObj = GameObject.Find("character");
        if (playerObj != null)
            player = playerObj.GetComponent<Player>();
        else
            Debug.LogError(" 'character' 오브젝트를 찾을 수 없습니다!");
    }

    void Update()
    {
        if (player != null && player.AutoMode == false)
        {
            if (OnShake == true) return;
            rotaX += -(Input.GetAxis("Mouse Y")) * MouseSensor * Time.deltaTime;
            rotaY += Input.GetAxis("Mouse X") * MouseSensor * Time.deltaTime;
            rotaX = Mathf.Clamp(NormalizeAngle(rotaX), -LimitAngle, LimitAngle);
            Quaternion root = Quaternion.Euler(rotaX, rotaY, 0);
            transform.rotation = root;
        }
        else
        {
            rotaX = 14;
            rotaY = 46;
            Quaternion root = Quaternion.Euler(rotaX, rotaY, 0);
            transform.rotation = root;
        }
    }

    void LateUpdate()
    {
        if (Camera == null || player == null) return;
        if (player.IsAttacking == true) return;
        transform.position = Vector3.MoveTowards(transform.position, Target.position, FollowSpeed * Time.deltaTime);


        float targetDis = Input.GetKey(KeyCode.Z) ? minDis + 1f :
                          player.IsAttacking ? minDis + 1f : maxDis;

        currentDis = Mathf.Lerp(currentDis, targetDis, Time.deltaTime * ZoomSmoothSpeed);
        finalDir = transform.TransformPoint(dir * currentDis);
        if (OnShake == true) return;
        RaycastHit hit;
        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDis = Mathf.Clamp(hit.distance, minDis, currentDis);
        }
        else
        {
            finalDis = currentDis;
        }

        Camera.localPosition = Vector3.Lerp(Camera.localPosition, dir * finalDis, Time.deltaTime * Smove);

        Debug.Log($"[] targetDis: {targetDis}, finalDis: {finalDis}, localPos: {Camera.localPosition}, IsAttacking: {player.IsAttacking}");
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
}
