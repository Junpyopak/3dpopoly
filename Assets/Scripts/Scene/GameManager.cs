using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UI_TITLE;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform sPoint;//������ ��ġ
    public int maxEnemy = 5;
    public int enemyCount;

    [Header("�� ����")]
    [SerializeField] List<GameObject> listEnemy;//���� ����

    [SerializeField] float spawnTime = 4.0f;
    [SerializeField] float sTimer = 0.0f;//����Ÿ�̸� 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawn();
    }
    static int testIndex = 0;//Ȯ�ο�
    public void CreateEnemy()
    {
        Vector3 newPos = sPoint.position;
        GameObject go = Instantiate(listEnemy[0], newPos, Quaternion.identity);

        go.name = $"monster {testIndex.ToString()}";
        testIndex++;
    }
   public void CheckSpawn()//������ �ִ� ���� ��ŭ ���� ������
    {

        if (enemyCount < maxEnemy)//�ִ� �� ������ ���� �� ���� ������ ������
        {
            sTimer += Time.deltaTime;
            if (sTimer >= spawnTime)//�� ��ȯŸ�̸Ӱ� ���ư��� �������ð��� �Ǹ� ������
            {
                sTimer = 0.0f;
                CreateEnemy();//�� ������
                enemyCount++;
            }
        }
    }
}
