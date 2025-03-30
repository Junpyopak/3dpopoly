using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public int enemyCount;
    public int maxEnemy = 3;
    public GameObject SpawnPoint;//������ ��ġ
    [SerializeField] float spawnTime = 4.0f;
    [SerializeField] float sTimer = 0.0f;//����Ÿ�̸� 
    BoxCollider SpawnBoxCollider;//Ư�� ���� �������� ������ �ɼ��ֵ���
    [System.Serializable]
    public class pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private void Awake()
    {
        SpawnBoxCollider = SpawnPoint.GetComponent<BoxCollider>();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                Vector3 Pos = SpawnPoint.transform.position;//���� ��ġ�� �ڽ� �ݶ��̴��� ��ġ�� ����
                float RandomX = SpawnBoxCollider.bounds.size.x;//�ڽ��ݶ��̴��� ������ �ȿ����� ������ �Ͻ����� x,z ����� ����
                float RandomZ = SpawnBoxCollider.bounds.size.z;

                RandomX = Random.Range((RandomX / 2) * -1, RandomX / 2);//�����Լ��� ����� �ݶ��̴� ����� ������ ������,
                                                                        //-1�� ���� ������ ������ ���� ���� �������� ������ġ ����
                RandomZ = Random.Range((RandomZ / 2) * -1, RandomZ / 2);
                Vector3 newPos = new Vector3(RandomX, 0f, RandomZ);
                Vector3 SpawnPos = Pos + newPos;//���� �ڽ��ݶ��̴��� ��ġ���� ���� ������ ��ġ�� �����Բ�
                GameObject obj = Instantiate(pool.prefab, SpawnPos, Quaternion.identity);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    private void Update()
    {
        //if (enemyCount < maxEnemy)//,�ִ� �� ������ ���� �� ���� ������ ������
        //{
        //    sTimer += Time.deltaTime;
        //    if (sTimer >= spawnTime)//�� ��ȯŸ�̸Ӱ� ���ư��� �������ð��� �Ǹ� ������
        //    {
        //        sTimer = 0.0f;
        //        EnemySpawn();
        //        enemyCount++;
        //    }
        //}
        
    }
    private void EnemySpawn()
    {
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                Vector3 Pos = SpawnPoint.transform.position;//���� ��ġ�� �ڽ� �ݶ��̴��� ��ġ�� ����
                float RandomX = SpawnBoxCollider.bounds.size.x;//�ڽ��ݶ��̴��� ������ �ȿ����� ������ �Ͻ����� x,z ����� ����
                float RandomZ = SpawnBoxCollider.bounds.size.z;

                RandomX = Random.Range((RandomX / 2) * -1, RandomX / 2);//�����Լ��� ����� �ݶ��̴� ����� ������ ������,
                                                                        //-1�� ���� ������ ������ ���� ���� �������� ������ġ ����
                RandomZ = Random.Range((RandomZ / 2) * -1, RandomZ / 2);
                Vector3 newPos = new Vector3(RandomX, 0f, RandomZ);
                Vector3 SpawnPos = Pos + newPos;//���� �ڽ��ݶ��̴��� ��ġ���� ���� ������ ��ġ�� �����Բ�
                GameObject obj = Instantiate(pool.prefab, SpawnPos, Quaternion.identity);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        GameObject obj = poolDictionary[tag].Dequeue();
        poolDictionary[tag].Enqueue(obj);

        return obj;
    }
}
