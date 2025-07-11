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
    public Dictionary<GameObject, Vector3> enemySpawnPositions = new Dictionary<GameObject, Vector3>();
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
        if (enemy == null)
        {
            Debug.LogWarning("스폰 실패: Enemy 오브젝트가 null입니다.");
            return;
        }
        if (enemy != null)
        {
            Vector3 safePos = GetSafeSpawnPosition();
            if (safePos == Vector3.negativeInfinity) return; // 실패 방지

            enemy.transform.position = safePos;
            enemy.GetComponent<Enemy>().ResetEnemy();

            SkinnedMeshRenderer skinnedRenderer = enemy.GetComponentInChildren<SkinnedMeshRenderer>();//몬스터 재 스폰시 알베도 다시 정상으로
            if (skinnedRenderer != null)
            {
                foreach (var mat in skinnedRenderer.materials)
                {
                    // 알베도 컬러 = #CFCFCF
                    if (mat.HasProperty("_Color"))
                        mat.color = new Color(0.8113208f, 0.8113208f, 0.8113208f);

                    // Smoothness = 0.413
                    if (mat.HasProperty("_Glossiness"))
                        mat.SetFloat("_Glossiness", 0.413f);
                }
            }

            enemy.SetActive(true);
            activeEnemyPositions.Add(safePos);
            enemySpawnPositions[enemy] = safePos; // 적 오브젝트와 스폰 위치 연결s;
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
            if (!obj.activeInHierarchy) // 비활성화된 것만 사용
            {
                poolQueue.Enqueue(obj);
                return obj;
            }
            poolQueue.Enqueue(obj); // 다시 뒤로 보내기
        }

        Debug.LogWarning($"사용 가능한 {tag} 오브젝트가 없습니다.");
        return null;
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        activeEnemyPositions.RemoveAll(pos => Vector3.Distance(pos, enemy.transform.position) < 0.1f);
       // enemy.SetActive(false);
        if (enemySpawnPositions.ContainsKey(enemy))
        {
            Vector3 spawnPos = enemySpawnPositions[enemy];
            activeEnemyPositions.Remove(spawnPos);
            enemySpawnPositions.Remove(enemy);
        }
        StartCoroutine(DeathAnim(enemy));
        //enemy.SetActive(false);
        //enemyCount--;
    }
    private IEnumerator DeathAnim(GameObject enemy)
    {
        Animator animator = enemy.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("isDeath"); // 트리거 이름은 실제 애니메이션 트리거에 맞게 수정
        }
        SkinnedMeshRenderer skinnedRenderer = enemy.GetComponentInChildren<SkinnedMeshRenderer>();
        if (skinnedRenderer != null)
        {
            foreach (var mat in skinnedRenderer.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    mat.color = Color.black;
                }
                if (mat.HasProperty("_Glossiness"))
                {
                    mat.SetFloat("_Glossiness", 0f);
                }
            }
        }
        // 애니메이션이 재생되는 시간만큼 대기 (예: 2초)
        yield return new WaitForSeconds(3.8f);

        // 비활성화 처리
        enemy.SetActive(false);
    }
}
