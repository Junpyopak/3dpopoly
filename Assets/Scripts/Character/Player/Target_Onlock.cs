using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Onlock : MonoBehaviour
{
    //public float detectionDis = 7f;     // 탐지 거리
    //public string enemyTag = "Enemy";   // 적 태그

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        GameObject nearestEnemy = FindNearestEnemy();
    //        if (nearestEnemy != null)
    //        {
    //            // 가장 가까운 적을 바라봄
    //            transform.LookAt(nearestEnemy.transform);
    //        }
    //    }
    //}

    //GameObject FindNearestEnemy()
    //{
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    //    GameObject nearest = null;
    //    float minDist = detectionDis;

    //    foreach (GameObject enemy in enemies)
    //    {
    //        float dist = Vector3.Distance(transform.position, enemy.transform.position);
    //        if (dist < minDist)
    //        {
    //            minDist = dist;
    //            nearest = enemy;
    //        }
    //    }

    //    return nearest;
    //}

    //// 디버깅용 Gizmo
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, detectionDis);

    //    GameObject nearest = FindNearestEnemy();
    //    if (nearest != null)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(transform.position, nearest.transform.position);
    //    }
    //}
    public float detectionDis = 7f;
    public string enemyTag = "Enemy";
    private GameObject lockedTarget;

    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (lockedTarget == null)
            {
                lockedTarget = FindNearestEnemy();
            }
        }
        else
        {
            lockedTarget = null; // X 키를 떼면 즉시 해제
        }
    }

    public bool IsLockingOn()
    {
        return lockedTarget != null;
    }

    public GameObject GetLockedTarget()
    {
        return lockedTarget;
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearest = null;
        float minDist = detectionDis;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    // 선택적: 시각적 디버깅
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionDis);

        if (lockedTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, lockedTarget.transform.position);
        }
    }
}
