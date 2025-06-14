using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Onlock : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;                      
    public LayerMask targetLayer;
    public float detectRange = 20f;

    public GameObject lockOnSpritePrefab;

    private Transform currentTarget;
    private GameObject lockOnInstance;

    void Update()
    {
        
        Vector3 rayStart = player.position + Vector3.up * 1f;
        Vector3 rayDirection = player.forward;

        Debug.DrawRay(rayStart, rayDirection * detectRange, Color.red);

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentTarget == null)
                TryLockOn(rayStart, rayDirection);
            else
                ReleaseLockOn();
        }

        if (lockOnInstance && currentTarget)
        {
            lockOnInstance.transform.position = currentTarget.position + Vector3.up * 2f;
            lockOnInstance.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        }
    }

    void TryLockOn(Vector3 rayStart, Vector3 rayDirection)
    {
        Ray ray = new Ray(rayStart, rayDirection);

        if (Physics.Raycast(ray, out RaycastHit hit, detectRange, targetLayer))
        {
            currentTarget = hit.transform;
            mainCamera.transform.LookAt(currentTarget);

            lockOnInstance = Instantiate(lockOnSpritePrefab);
            lockOnInstance.transform.position = currentTarget.position + Vector3.up * 2f;
        }
    }

    void ReleaseLockOn()
    {
        currentTarget = null;
        if (lockOnInstance)
            Destroy(lockOnInstance);
    }
}
