using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPool : MonoBehaviour
{
    //public static TextPool Instance;

    //public GameObject damageTextPrefab;
    //public int poolSize = 50;

    //private Queue<GameObject> pool = new Queue<GameObject>();

    //void Awake()
    //{
    //    Instance = this;

    //    for (int i = 0; i < poolSize; i++)
    //    {
    //        GameObject obj = Instantiate(damageTextPrefab);
    //        obj.SetActive(false);
    //        pool.Enqueue(obj);
    //    }
    //}

    //public GameObject Get()
    //{
    //    GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(damageTextPrefab);
    //    obj.SetActive(true);
    //    return obj;
    //}

    //public void Return(GameObject obj)
    //{
    //    obj.SetActive(false);
    //    pool.Enqueue(obj);
    //}
    public static TextPool Instance;

    public GameObject damageTextPrefab;
    public int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(damageTextPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        GameObject obj = null;

        // DestroyµÈ °´Ã¼°¡ ³ª¿À¸é ¹«½ÃÇÏ°í »õ·Î ²¨³¿
        while (pool.Count > 0 && obj == null)
        {
            obj = pool.Dequeue();
        }

        if (obj == null)
        {
            obj = Instantiate(damageTextPrefab, transform);
        }

        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj)
    {
        if (obj == null) return;

        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
