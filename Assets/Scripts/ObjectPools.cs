using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    [SerializeField] private List<PoolEntry> initialPools;
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    private Dictionary<GameObject, GameObject> instanceToPrefab = new Dictionary<GameObject, GameObject>();

    public static ObjectPools instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //foreach (PoolEntry entry in initialPools)
        //{
        //CreatePools(entry.prefab, entry.poolSize);
        //}
        for (int i = 0; i < initialPools.Count; i++)
        {
            PoolEntry entry = initialPools[i];
            CreatePools(entry.prefab, entry.poolSize);
        }
    }

    public void CreatePools(GameObject prefab, int size)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab] = new Queue<GameObject>();
        }

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            poolDictionary[prefab].Enqueue(obj);
            instanceToPrefab[obj] = prefab;
        }
    }

    public GameObject GetFromPool(GameObject prefab)
    {
        if (poolDictionary[prefab].Count > 0)
        {
            GameObject obj = poolDictionary[prefab].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(true);
            instanceToPrefab[obj] = prefab;
            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        if (!instanceToPrefab.ContainsKey(obj))
        {
            Destroy(obj);
            return;
        }

        GameObject prefab = instanceToPrefab[obj];
        obj.SetActive(false);
        poolDictionary[prefab].Enqueue(obj);
    }

    [System.Serializable]
    public class PoolEntry
    {
        public GameObject prefab;
        public int poolSize;
    }
}
