using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject m_prefabObject;
    [SerializeField] private int m_poolDepth;

    private GameObject m_pooledObject;

    private readonly List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < m_poolDepth; i++)
        {
            CreateNewObject();
        }
    }

    public GameObject GetAvailableObject()
    {
        //return available object
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                return pool[i];
            }
        }

        //if pool is exhausted, make new ones
        CreateNewObject();

        return m_pooledObject;
    }

    private void CreateNewObject()
    {
        m_pooledObject = Instantiate(m_prefabObject);

        m_pooledObject.SetActive(false);
        pool.Add(m_pooledObject);
    }
}

