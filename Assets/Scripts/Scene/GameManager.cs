using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UI_TITLE;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int maxEnemy = 5;
    public int enemyCount;

    public GameObject SpawnPoint;//������ ��ġ
    BoxCollider SpawnBoxCollider;//Ư�� ���� �������� ������ �ɼ��ֵ���

    [Header("�� ����")]
    [SerializeField] List<GameObject> listEnemy;//���� ����

    [SerializeField] float spawnTime = 4.0f;
    [SerializeField] float sTimer = 0.0f;//����Ÿ�̸� 
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
    static int testIndex = 0;//Ȯ�ο�
    public void CreateEnemy()
    {
        Vector3 Pos = SpawnPoint.transform.position;//���� ��ġ�� �ڽ� �ݶ��̴��� ��ġ�� ����
        float RandomX = SpawnBoxCollider.bounds.size.x;//�ڽ��ݶ��̴��� ������ �ȿ����� ������ �Ͻ����� x,z ����� ����
        float RandomZ = SpawnBoxCollider.bounds.size.z;

        RandomX = Random.Range((RandomX / 2) * -1, RandomX / 2);//�����Լ��� ����� �ݶ��̴� ����� ������ ������,
        //-1�� ���� ������ ������ ���� ���� �������� ������ġ ����
        RandomZ = Random.Range((RandomZ / 2) * -1, RandomZ / 2);
        Vector3 newPos = new Vector3(RandomX,0f,RandomZ);
        Vector3 SpawnPos = Pos + newPos;//���� �ڽ��ݶ��̴��� ��ġ���� ���� ������ ��ġ�� �����Բ�
        GameObject go = Instantiate(listEnemy[0], SpawnPos, Quaternion.identity);

        //go.name = $"Warrok {testIndex.ToString()}";
        go.name = "Warrok";
        testIndex++;
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
}
