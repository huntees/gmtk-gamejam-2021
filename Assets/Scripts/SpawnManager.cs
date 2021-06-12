using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] GameObject[] m_enemyPrefabs;
    private int m_randomEnemyIndex = 0;
    private GameObject m_instantiatedEnemy;

    [Header("Enemy Spawn Locations")]
    [SerializeField] private Transform[] m_spawnLocations;
    private int m_randomLocationIndex;

    [Header("Enemy Spawn Properties")]
    [SerializeField] private float m_maxSpawnTime = 8f;
    [SerializeField] private float m_minSpawnTime = 4f;
    private float m_randomSpawnTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        InitiateEnemySpawn(3, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateEnemySpawn(int numOfEnemies, int enemiesPerSpawn)
    {
        StartCoroutine(SpawnEnemies(numOfEnemies, enemiesPerSpawn));
    }

    IEnumerator SpawnEnemies(int numOfEnemies, int enemiesPerSpawn)
    {
        int enemiesSpawned = 0;

        while (enemiesSpawned < numOfEnemies)
        {
            m_randomSpawnTime = Random.Range(m_minSpawnTime, m_maxSpawnTime);

            yield return new WaitForSeconds(m_randomSpawnTime);

            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                //Randomise spawn location and enemy type
                m_randomLocationIndex = Random.Range(0, m_spawnLocations.Length);
                m_randomEnemyIndex = Random.Range(0, m_enemyPrefabs.Length);

                //Get any enemy and set their location
                m_instantiatedEnemy = Instantiate(m_enemyPrefabs[m_randomEnemyIndex], transform.position, transform.rotation);
                m_instantiatedEnemy.transform.position = m_spawnLocations[m_randomLocationIndex].position;

                m_instantiatedEnemy.SetActive(true);

                enemiesSpawned++;

                //break if enemies spawned exceeds limit
                if (enemiesSpawned >= numOfEnemies)
                {
                    break;
                }
            }
        }
    }
}
