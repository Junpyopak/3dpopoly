using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BattleManager : MonoBehaviour
{
    public static GameManager Instance;
    public int maxEnemy = 5;
    public int enemyCount;
    public int KillCount;
    public int MaxValue = 10;
    public GameObject SpawnPoint;//리스폰 위치
    public GameObject PotalPoint;//랜덤 포탈 소환 구간
    BoxCollider PotalBoxCollider;
    public Text KillText;
    private int PotalCount = 0;
    private int MaxPotal = 1;
    [SerializeField] List<GameObject> listPotal;//포탈

    void Start()
    {
        enemyCount = 1;
        PotalBoxCollider = PotalPoint.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBossAcquire();
        checkPotal();
    }
    static int testIndex = 0;//확인용


    public void CheckBossAcquire()
    {
        KillText.text = $"{((int)KillCount).ToString("D2")} / {((int)MaxValue).ToString("D2")}";
    }
    public void CreatePotal()
    {
        Vector3 basePos = PotalPoint.transform.position;
        float rangeX = PotalBoxCollider.bounds.size.x;
        float rangeZ = PotalBoxCollider.bounds.size.z;

        float randomX = Random.Range(-rangeX / 2, rangeX / 2);
        float randomZ = Random.Range(-rangeZ / 2, rangeZ / 2);

        Vector3 spawnOffset = new Vector3(randomX, 0f, randomZ); // yOffset 적용
        Vector3 spawnPos = basePos + spawnOffset;
        spawnPos.y = 18f;
        GameObject portal = Instantiate(listPotal[0], spawnPos, Quaternion.identity);
    }
    public void checkPotal()
    {
        if(KillCount>=MaxValue)
        {
            if (PotalCount < MaxPotal)
            {
                CreatePotal();
                PotalCount++;
            }
        }     
    }
}
