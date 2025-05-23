using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_unbeatable : MonoBehaviour
{
    public static Boss_unbeatable instance;
    public int enemyCount;
    public int maxEnemy = 3;
    public GameObject SpawnPoint; // ������ ��ġ
    [SerializeField] float spawnTime = 1.0f;
    [SerializeField] float sTimer = 0.0f; // ���� Ÿ�̸�
    BoxCollider SpawnBoxCollider;

    public float minSpawnDistance = 2.0f; // �ּ� �Ÿ�
    public List<Vector3> activeEnemyPositions = new List<Vector3>(); // ���� �� ��ġ ���� ����Ʈ
    public Dictionary<GameObject, Vector3> enemySpawnPositions = new Dictionary<GameObject, Vector3>();
    private bool isSpawn = false;
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
                GameObject obj = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (!isSpawn) return;
        if (enemyCount < maxEnemy)
        {
            sTimer += Time.deltaTime;
            if (sTimer >= spawnTime)
            {
                sTimer = 0.0f;
                EnemySpawn();
                enemyCount++;
            }
        }
    }

    private void EnemySpawn()
    {

        GameObject enemy = SpawnFromPool("Enemy");
        if (enemy == null)
        {
            Debug.LogWarning("���� ����: Enemy ������Ʈ�� null�Դϴ�.");
            return;
        }
        if (enemy != null)
        {
            Vector3 safePos = GetSafeSpawnPosition();
            if (safePos == Vector3.negativeInfinity) return; // ���� ����

            enemy.transform.position = safePos;
            enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            enemy.GetComponent<Enemy>().ResetEnemy();
            enemy.SetActive(true);
            activeEnemyPositions.Add(safePos);
            enemySpawnPositions[enemy] = safePos; // �� ������Ʈ�� ���� ��ġ ����s;
        }
    }

    private Vector3 GetSafeSpawnPosition()
    {
        Vector3 pos;
        int attempts = 0;

        do
        {
            pos = GetRandomPosition();
            attempts++;
        }
        while (!IsFarEnoughFromOthers(pos) && attempts < 20);

        return pos;
    }

    private bool IsFarEnoughFromOthers(Vector3 newPos)
    {
        foreach (var existing in activeEnemyPositions)
        {
            if (Vector3.Distance(newPos, existing) < minSpawnDistance)
                return false;
        }
        return true;

    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePos = SpawnPoint.transform.position;
        float sizeX = SpawnBoxCollider.bounds.size.x;
        float sizeZ = SpawnBoxCollider.bounds.size.z;

        float randX = Random.Range(-sizeX / 2, sizeX / 2);
        float randZ = Random.Range(-sizeZ / 2, sizeZ / 2);

        return new Vector3(basePos.x + randX, basePos.y, basePos.z + randZ);
    }

    public GameObject SpawnFromPool(string tag)
    {
        //if (!poolDictionary.ContainsKey(tag))
        //    return null;

        //GameObject obj = poolDictionary[tag].Dequeue();
        //poolDictionary[tag].Enqueue(obj);

        //return obj;
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = null;
        Queue<GameObject> poolQueue = poolDictionary[tag];

        for (int i = 0; i < poolQueue.Count; i++)
        {
            obj = poolQueue.Dequeue();
            if (!obj.activeInHierarchy) // ��Ȱ��ȭ�� �͸� ���
            {
                poolQueue.Enqueue(obj);
                return obj;
            }
            poolQueue.Enqueue(obj); // �ٽ� �ڷ� ������
        }

        Debug.LogWarning($"��� ������ {tag} ������Ʈ�� �����ϴ�.");
        return null;
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        activeEnemyPositions.RemoveAll(pos => Vector3.Distance(pos, enemy.transform.position) < 0.1f);
        enemy.SetActive(false);
        if (enemySpawnPositions.ContainsKey(enemy))
        {
            Vector3 spawnPos = enemySpawnPositions[enemy];
            activeEnemyPositions.Remove(spawnPos);
            enemySpawnPositions.Remove(enemy);
        }

        //enemy.SetActive(false);
        //enemyCount--;
    }
    public void StartSpawn()
    {
        isSpawn = true;
    }
    public void StopSpawn()
    {
        isSpawn=false;
        sTimer = 0.0f;
    }
}
