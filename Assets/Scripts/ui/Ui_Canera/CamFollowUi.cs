using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowUi : MonoBehaviour
{
    public Transform Target;
    public float FollowSpeed = 10f;
    public float MouseSensor = 100f;//마우스 감도
    public float LimitAngle = 70f;//제한각도

    private float rotaX;
    private float rotaY;

    public Transform Camera;
    public Vector3 dir;
    public Vector3 finalDir;//마지막거리
    public float minDis;//최소
    public float maxDis;//최대
    public float finalDis;
    public float Smove = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rotaX = transform.localRotation.eulerAngles.x;
        rotaY = transform.localRotation.eulerAngles.y;
        dir = Camera.localPosition.normalized;
        finalDis = Camera.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotaX += -(Input.GetAxis("Mouse Y")) * MouseSensor * Time.deltaTime;
        rotaY += Input.GetAxis("Mouse X") * MouseSensor * Time.deltaTime;


        rotaX = Mathf.Clamp(rotaX, -LimitAngle, LimitAngle);
        Quaternion root = Quaternion.Euler(rotaX,rotaY,0);
        transform.rotation = root;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, FollowSpeed * Time.deltaTime);
        finalDir = transform.TransformPoint(dir * maxDis);

        RaycastHit hit;
        if(Physics.Linecast(transform.position,finalDir,out hit))
        {
            finalDis = Mathf.Clamp(hit.distance,minDis,maxDis);
        }
        else
        {
            finalDis = maxDis;
        }

        Camera.localPosition = Vector3.Lerp(Camera.localPosition,dir*finalDis, Time.deltaTime * Smove);
    }
}
