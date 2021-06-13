using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpawnManager m_spawnManager;
    protected int damagePoints = 15;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 2.3f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual int GetDamagePoints()
    {
        return damagePoints;
    }

    public virtual void Die()
    {
        m_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        m_spawnManager.DecreaseEnemyCount();

        //should move to children after testing
        m_spawnManager.AddScore(10);

        Destroy(gameObject);

    }
}
