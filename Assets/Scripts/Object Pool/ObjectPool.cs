using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    [SerializeField]
    private string name;
    [SerializeField]
    private GameObject objectToPool;
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private bool expansible;

    [SerializeField]
    private GameObject[] pool;

    private GameObject hierarchy;

    /// <summary>
    /// Expands the size of a pool by 1
    /// </summary>
    /// <param name="pool"> The pool to expand </param>
    /// <returns> The expanded pool </returns>
    private GameObject[] ForceExpandRequest(GameObject[] pool)
    {
        poolSize++;
        GameObject[] expandedPool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            expandedPool[i] = pool[i];
        }

        GameObject newObj = (GameObject)GameObject.Instantiate(objectToPool);
        newObj.SetActive(false);
        newObj.transform.parent = hierarchy.transform;
        expandedPool[expandedPool.Length - 1] = newObj;

        return expandedPool;
    }

    /// <summary>
    /// Sets inactive a specific object in the Pool
    /// </summary>
    /// <param name="id"> The position of the object in the pool </param>
    public void PoolReturn(int id)
    {
        pool[id].SetActive(false);
    }

    /// <summary>
    /// Sets inactive the first active object in the Pool
    /// </summary>
    public void PoolReturn()
    {
        int i = 0;
        while(!pool[i].activeInHierarchy && i < pool.Length - 1)
        {
            i++;
        }
        pool[i].SetActive(false);
    }

    /// <summary>
    /// Sets active the first inactive object in the Pool at the specified transform.
    /// </summary>
    /// <param name="position"> Position of the object </param>
    /// <param name="rotation"> Rotation of the object </param>
    /// <param name="scale"> Scale of the object </param>
    public void PoolRequest(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        int i = 0;
        while (pool[i].activeInHierarchy && i < pool.Length - 1)
        {
            i++;
        }
        if(expansible && i == pool.Length - 1)
        {
            pool = ForceExpandRequest(pool);
        }

        if (!pool[i].activeInHierarchy)
        {
            pool[i].transform.position = position;
            pool[i].transform.Rotate(rotation);
            pool[i].transform.localScale = scale;
            pool[i].SetActive(true);
        }
    }

    /// <summary>
    /// Get the GameObject that the Object Pool is pooling
    /// </summary>
    /// <returns> GameObject value </returns>
    public GameObject GetPoolObject()
    {
        return objectToPool;
    }

    /// <summary>
    /// Get the name value of the Object Pool
    /// </summary>
    /// <returns> string name value </returns>
    public string GetName()
    {
        return name;
    }

    /// <summary>
    /// Initializes the pool instantiating the specified amount of objects of the specific GameObject as Inactive.
    /// </summary>
    public void Initialize()
    {
        hierarchy = new GameObject(name);
        hierarchy.transform.parent = PoolManager.Instance.GetRoot().transform;

        pool = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++) 
        {
            GameObject obj = (GameObject)GameObject.Instantiate(objectToPool);
            obj.SetActive(false);
            obj.transform.parent = hierarchy.transform;
            pool[i] = obj;
        }
    }

}
