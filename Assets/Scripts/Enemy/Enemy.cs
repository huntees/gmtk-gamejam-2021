using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpawnManager m_spawnManager;
    protected int damagePoints = 15;

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

        Destroy(gameObject);
    }

    protected void AddScore(int score)
    {
        m_spawnManager.AddScore(score);
    }
}
