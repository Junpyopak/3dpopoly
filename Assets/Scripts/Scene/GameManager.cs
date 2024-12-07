using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UI_TITLE;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform sPoint;//리스폰 위치
    public int maxEnemy = 5;
    public int enemyCount;

    [Header("적 생성")]
    [SerializeField] List<GameObject> listEnemy;//적의 종류

    [SerializeField] float spawnTime = 4.0f;
    [SerializeField] float sTimer = 0.0f;//스폰타이머 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawn();
    }
    static int testIndex = 0;//확인용
    public void CreateEnemy()
    {
        Vector3 newPos = sPoint.position;
        GameObject go = Instantiate(listEnemy[0], newPos, Quaternion.identity);

        go.name = $"monster {testIndex.ToString()}";
        testIndex++;
    }
   public void CheckSpawn()//설정된 최대 적수 많큼 적을 리스폰
    {

        if (enemyCount < maxEnemy)//최대 적 수보다 현재 적 수가 적으면 리스폰
        {
            sTimer += Time.deltaTime;
            if (sTimer >= spawnTime)//적 소환타이머가 돌아가고 리스폰시간이 되면 리스폰
            {
                sTimer = 0.0f;
                CreateEnemy();//적 리스폰
                enemyCount++;
            }
        }
    }
}
