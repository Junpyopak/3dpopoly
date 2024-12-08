using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UI_TITLE;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int maxEnemy = 5;
    public int enemyCount;

    public GameObject SpawnPoint;//리스폰 위치
    BoxCollider SpawnBoxCollider;//특정 구간 내에서만 스폰이 될수있도록

    [Header("적 생성")]
    [SerializeField] List<GameObject> listEnemy;//적의 종류

    [SerializeField] float spawnTime = 4.0f;
    [SerializeField] float sTimer = 0.0f;//스폰타이머 
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 1;
        SpawnBoxCollider = SpawnPoint.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawn();
    }
    static int testIndex = 0;//확인용
    public void CreateEnemy()
    {
        Vector3 Pos = SpawnPoint.transform.position;//현재 위치를 박스 콜라이더의 위치로 설정
        float RandomX = SpawnBoxCollider.bounds.size.x;//박스콜라이더의 사이즈 안에서만 리스폰 하시위해 x,z 사이즈를 설정
        float RandomZ = SpawnBoxCollider.bounds.size.z;

        RandomX = Random.Range((RandomX / 2) * -1, RandomX / 2);//랜덤함수를 사용해 콜라이더 사이즈를 반으로 나누고,
        //-1을 곱한 값부터 곱하지 않은 값이 랜덤으로 스폰위치 설정
        RandomZ = Random.Range((RandomZ / 2) * -1, RandomZ / 2);
        Vector3 newPos = new Vector3(RandomX,0f,RandomZ);
        Vector3 SpawnPos = Pos + newPos;//기존 박스콜라이더의 위치에서 위에 선언한 위치로 나오게끔
        GameObject go = Instantiate(listEnemy[0], SpawnPos, Quaternion.identity);

        //go.name = $"Warrok {testIndex.ToString()}";
        go.name = "Warrok";
        testIndex++;
    }
   public void CheckSpawn()//설정된 최대 적수 많큼 적을 리스폰
    {

        if (enemyCount < maxEnemy)//,최대 적 수보다 현재 적 수가 적으면 리스폰
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
