using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    /* //the object
     public GameObject satellitePrefab;
     public int spawnCount;
     //list of objects to be used
     public List<GameObject> satelliteList;

     private void Start()
     {
         //spawn amount of satellites and add them to the list
         for (int i = 0; i < spawnCount; i++)
         {
             GameObject satellite = Instantiate(satellitePrefab);
             satelliteList.Add(satellite);
             satellite.transform.parent = this.transform;
             satellite.SetActive(false);
         }

     }*/
    [System.Serializable]
    public class pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static PoolManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectpool);
        }
    }

    public GameObject spawnFromPool(string tag, Vector3 pos, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag" + tag + "does not exist");
            return null;
        }
        
       GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
