using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Onlock : MonoBehaviour
{
    public float detectionDis = 7f;     // Ž�� �Ÿ�
    public string enemyTag = "Enemy";   // �� �±�

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                // ���� ����� ���� �ٶ�
                transform.LookAt(nearestEnemy.transform);
            }
        }
    }

    GameObject FindNearestEnemy()
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

    // ������ Gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionDis);

        GameObject nearest = FindNearestEnemy();
        if (nearest != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, nearest.transform.position);
        }
    }
}
