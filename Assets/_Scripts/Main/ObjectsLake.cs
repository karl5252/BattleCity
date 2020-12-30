using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ObjectLakeItem
{
    [Header("Options")]
    public GameObject objectToBePooled;
    public int amountToPool;
    public bool isExpandable;
}


public class ObjectsLake : MonoBehaviour
{
    public static ObjectsLake SharedInstance;
    public List<ObjectLakeItem> itemsToPool;
    public List<GameObject> pooledObjects;

    void Awake()
    {
        SharedInstance = this;
    }

    // Use this for initialization
    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectLakeItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToBePooled);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectLakeItem item in itemsToPool)
        {
            if (item.objectToBePooled.tag == tag)
            {
                if (item.isExpandable)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToBePooled);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }


}
