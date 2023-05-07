using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviourSingleton<PoolManager>
{
    [SerializeField]
    private ObjectPool[] poolManager;

    private GameObject root;

    // Destroy

    /// <summary>
    /// Sets inactive a specific object inside a pool of the specified position in the poolManager array.
    /// </summary>
    /// <param name="poolID"> The position of the pool in the poolManager array </param>
    /// <param name="objectID"> The position of the object in the Object Pool </param>
    public void DestroyObject(int poolID, int objectID)
    {
        if (poolManager[poolID] != null)
        {
            poolManager[poolID].PoolReturn(objectID);
        }
    }

    /// <summary>
    /// Sets inactive the first inactive object inside a pool of the specified position in the poolManager array.
    /// </summary>
    /// <param name="poolID"> The position of the pool in the poolManager array </param>
    public void DestroyObject(int poolID)
    {
        if (poolManager[poolID] != null)
        {
            poolManager[poolID].PoolReturn();
        }
    }

    /// <summary>
    /// Sets inactive a specific object inside a pool of the specified GameObject.
    /// </summary>
    /// <param name="poolObject"> The GameObject value of the Object Pool </param>
    /// <param name="objectID"> The position of the object in the Object Pool </param>
    public void DestroyObject(GameObject poolObject, int objectID)
    {
        for (int i = 0; i < poolManager.Length; i++)
        {
            if (poolManager[i].GetPoolObject() == poolObject)
            {
                poolManager[i].PoolReturn(objectID);
            }
        }
    }

    /// <summary>
    /// Sets inactive the first inactive object inside a pool of the specified GameObject.
    /// </summary>
    /// <param name="poolObject"> The GameObject value of the Object Pool </param>
    public void DestroyObject(GameObject poolObject)
    {
        for (int i = 0; i < poolManager.Length; i++)
        {
            if (poolManager[i].GetPoolObject() == poolObject)
            {
                poolManager[i].PoolReturn();
            }
        }
    }


    /// <summary>
    /// Sets inactive a specific object inside a pool of the specified Name.
    /// </summary>
    /// <param name="poolName"> The name of the Object Pool </param>
    /// <param name="objectID"> The position of the object in the Object Pool </param>
    public void DestroyObject(string poolName, int objectID)
    {
        for (int i = 0; i < poolManager.Length; i++)
        {
            if (poolManager[i].GetName() == name)
            {
                poolManager[i].PoolReturn(objectID);
            }
        }
    }

    /// <summary>
    /// Sets inactive the first active object inside a pool of the specified Name.
    /// </summary>
    /// <param name="poolName"> The name of the Object Pool </param>
    public void DestroyObject(string poolName)
    {
        for (int i = 0; i < poolManager.Length; i++)
        {
            if (poolManager[i].GetName().ToLower() == name.ToLower())
            {
                poolManager[i].PoolReturn();
            }
        }
    }

    // Create

    /// <summary>
    /// Sets active the first inactive object inside a pool of the specified position in the poolManager array.
    /// </summary>
    /// <param name="poolID"> The position of the pool in the poolManager array </param>
    /// <param name="position"> The position of the object to create </param>
    /// <param name="rotation"> The rotation of the object to create </param>
    /// <param name="scale"> The scale of the object to create </param>
    public void CreateObject(int poolID, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        if (poolManager[poolID] != null)
        {
            poolManager[poolID].PoolRequest(position, rotation, scale);
        }
    }

    /// <summary>
    /// Sets active the first inactive object inside of a pool of the speficied GameObject.
    /// </summary>
    /// <param name="poolObject"> The GameObject value of the pool </param>
    /// <param name="position"> The position of the object to create </param>
    /// <param name="rotation"> The rotation of the object to create </param>
    /// <param name="scale"> The scale of the object to create </param>
    public void CreateObject(GameObject poolObject, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        for (int i = 0; i < poolManager.Length; i++)
        {
            if (poolManager[i].GetPoolObject() == poolObject)
            {
                poolManager[i].PoolRequest(position, rotation, scale);
            }
        }
    }

    /// <summary>
    /// Sets active the first inactive object inside of a pool of the speficied Name.
    /// </summary>
    /// <param name="poolName"> The name of the Object Pool </param>
    /// <param name="position"> The position of the object to create </param>
    /// <param name="rotation"> The rotation of the object to create </param>
    /// <param name="scale"> The scale of the object to create </param>
    public void CreateObject(string poolName, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        for (int i = 0; i < poolManager.Length; i++)
        {
            if (poolManager[i].GetName().ToLower() == name.ToLower())
            {
                poolManager[i].PoolRequest(position, rotation, scale);
            }
        }
    }

    // --

    /// <summary>
    /// Gets the root object, mainly for organizing pooled objects.
    /// </summary>
    /// <returns></returns>
    public GameObject GetRoot()
    {
        return root;
    }

    protected override void SingletonAwakened()
    {
        root = new GameObject("Pooled Objects");

        for (int i = 0; i < poolManager.Length; i++)
        {
            poolManager[i].Initialize();
        }
    }
}
