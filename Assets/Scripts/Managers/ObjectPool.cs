using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }
    public GameObject ballPrefab;
    public int poolSize = 100;
    private List<GameObject> pool;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(ballPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // Optionally expand the pool if all objects are in use
        GameObject newObj = Instantiate(ballPrefab);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnToPool()
    {
        foreach (GameObject obj in pool)
        {
            if (obj.activeInHierarchy)
            {
                obj.SetActive(false);
                obj.transform.position = Vector3.zero; // Reset the position, or any other default position
            }
        }
    }

    
}
