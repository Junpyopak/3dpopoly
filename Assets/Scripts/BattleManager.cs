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
    public GameObject SpawnPoint;//������ ��ġ
    public GameObject PotalPoint;//���� ��Ż ��ȯ ����
    BoxCollider SpawnBoxCollider;//Ư�� ���� �������� ������ �ɼ��ֵ���
    BoxCollider PotalBoxCollider;
    public Text KillText;
    private int PotalCount =0;
    private int MaxPotal = 1;

    [Header("�� ����")]
    [SerializeField] List<GameObject> listEnemy;//���� ����
    [SerializeField] List<GameObject> listPotal;//��Ż

    [SerializeField] float spawnTime = 4.0f;
    [SerializeField] float sTimer = 0.0f;//����Ÿ�̸� 
    void Start()
    {
        enemyCount = 1;
        SpawnBoxCollider = SpawnPoint.GetComponent<BoxCollider>();
        PotalBoxCollider = PotalPoint.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawn();
        CheckBossAcquire();
        checkPotal();
    }
    static int testIndex = 0;//Ȯ�ο�
    public void CreateEnemy()
    {
        Vector3 Pos = SpawnPoint.transform.position;//���� ��ġ�� �ڽ� �ݶ��̴��� ��ġ�� ����
        float RandomX = SpawnBoxCollider.bounds.size.x;//�ڽ��ݶ��̴��� ������ �ȿ����� ������ �Ͻ����� x,z ����� ����
        float RandomZ = SpawnBoxCollider.bounds.size.z;

        RandomX = Random.Range((RandomX / 2) * -1, RandomX / 2);//�����Լ��� ����� �ݶ��̴� ����� ������ ������,
        //-1�� ���� ������ ������ ���� ���� �������� ������ġ ����
        RandomZ = Random.Range((RandomZ / 2) * -1, RandomZ / 2);
        Vector3 newPos = new Vector3(RandomX, 0f, RandomZ);
        Vector3 SpawnPos = Pos + newPos;//���� �ڽ��ݶ��̴��� ��ġ���� ���� ������ ��ġ�� �����Բ�
        GameObject go = Instantiate(listEnemy[0], SpawnPos, Quaternion.identity);

        //go.name = $"Warrok {testIndex.ToString()}";
        //testIndex++;
        go.name = "Warrok";

    }
    public void CheckSpawn()//������ �ִ� ���� ��ŭ ���� ������
    {
        if (enemyCount < maxEnemy)//,�ִ� �� ������ ���� �� ���� ������ ������
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
    public void CheckBossAcquire()
    {
        KillText.text = $"{((int)KillCount).ToString("D2")} / {((int)MaxValue).ToString("D2")}";
    }
    public void CreatePotal()
    {
        Vector3 Pos = PotalPoint.transform.position;//���� ��ġ�� �ڽ� �ݶ��̴��� ��ġ�� ����
        float RandomX = PotalBoxCollider.bounds.size.x;//�ڽ��ݶ��̴��� ������ �ȿ����� ������ �Ͻ����� x,z ����� ����
        float RandomZ = PotalBoxCollider.bounds.size.z;

        RandomX = Random.Range((RandomX / 2) * -1, RandomX / 2);//�����Լ��� ����� �ݶ��̴� ����� ������ ������,
        //-1�� ���� ������ ������ ���� ���� �������� ������ġ ����
        RandomZ = Random.Range((RandomZ / 2) * -1, RandomZ / 2);
        Vector3 newPos = new Vector3(RandomX, 0f, RandomZ);
        Vector3 SpawnPos = Pos + newPos;//���� �ڽ��ݶ��̴��� ��ġ���� ���� ������ ��ġ�� �����Բ�
        GameObject po = Instantiate(listPotal[0], SpawnPos, Quaternion.identity);
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
