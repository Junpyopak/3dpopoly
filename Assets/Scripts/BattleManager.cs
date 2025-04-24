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
    BoxCollider PotalBoxCollider;
    public Text KillText;
    private int PotalCount = 0;
    private int MaxPotal = 1;
    [SerializeField] List<GameObject> listPotal;//��Ż

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
    static int testIndex = 0;//Ȯ�ο�


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
