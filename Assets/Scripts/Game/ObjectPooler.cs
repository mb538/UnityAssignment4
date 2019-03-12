using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;

    public GameObject obj;
    public int poolSize = 20;

    private List<GameObject> objectPool;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    void Start() 
    {
        objectPool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject _obj = (GameObject)Instantiate(obj);
            _obj.SetActive(false);
            objectPool.Add(_obj);
        }
    }
    
    public GameObject GetObjectFromPool() 
    {
        for(int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }
    
        return null;
    }
}
