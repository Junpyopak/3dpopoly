using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling instance;
    public int enemyCount;
    public int maxEnemy = 3;
    public GameObject SpawnPoint; // 리스폰 위치
    [SerializeField] float spawnTime = 4.0f;
    [SerializeField] float sTimer = 0.0f; // 스폰 타이머
    BoxCollider SpawnBoxCollider;

    public float minSpawnDistance = 2.0f; // 최소 거리
    public List<Vector3> activeEnemyPositions = new List<Vector3>(); // 현재 적 위치 추적 리스트

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
        if (enemy != null)
        {
            Vector3 safePos = GetSafeSpawnPosition();
            enemy.transform.position = safePos;
            enemy.SetActive(true);
            activeEnemyPositions.Add(safePos);
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
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = poolDictionary[tag].Dequeue();
        poolDictionary[tag].Enqueue(obj);

        return obj;
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        activeEnemyPositions.RemoveAll(pos => Vector3.Distance(pos, enemy.transform.position) < 0.1f);
        enemy.SetActive(false);
        enemyCount--;
    }
}
