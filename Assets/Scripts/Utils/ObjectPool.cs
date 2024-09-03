using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;

    private List<GameObject> pooledObjects = new List<GameObject>();

    public void InitPool()
    {
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Object.Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        //In case pool is full create another batch 
        InitPool();

        return GetPooledObject();
    }
}
