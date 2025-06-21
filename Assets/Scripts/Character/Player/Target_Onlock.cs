using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Onlock : MonoBehaviour
{
  
    public float detectionDis = 7f;
    public string enemyTag = "Enemy";
    private GameObject lockedTarget;
    public GameObject TargetArrow;
    private GameObject targetArrow;

    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (lockedTarget == null)
            {
                lockedTarget = FindNearestEnemy();
                if (lockedTarget != null)
                {
                    if (lockedTarget != null && TargetArrow != null)
                    {
                        if (targetArrow == null)
                        {
                            targetArrow = Instantiate(TargetArrow);
                        }

                        targetArrow.SetActive(true);
                    }
                }
            }
            if (lockedTarget != null && targetArrow != null)
            {
                Vector3 headPos = lockedTarget.transform.position + Vector3.up * 3.0f;
                targetArrow.transform.position = headPos;


                targetArrow.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
                if (!lockedTarget.activeInHierarchy)
                {
                    lockedTarget = null;
                    //targetArrow.SetActive(false);
                    Destroy(targetArrow);
                }
            }
        }
        else
        {
            lockedTarget = null; // X 키를 떼면 즉시 해제
            if (targetArrow != null)
            {
                //targetArrow.SetActive(false);
                Destroy(targetArrow);
            }
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
