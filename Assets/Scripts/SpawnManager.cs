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

    // [SerializeField] private Transform[] m_spawnRectanglesEdges;

    private int m_randomLocationIndex;

    [Header("Enemy Spawn Properties")]
    [SerializeField] private float m_maxSpawnTime = 8f;
    [SerializeField] private float m_minSpawnTime = 4f;
    private float m_randomSpawnTime = 4f;

    private int m_enemiesLeft = 0;
    private bool m_isSpawning = false;

    private int waveCount = 0;


    public class Pair
    {
        public Transform transform1;
        public Transform transform2;

        public Pair(Transform trans1, Transform trans2)
        {
            transform1 = trans1;
            transform2 = trans2;
        }
    }

    private Pair pair1, pair2, pair3, pair4;

    // Start is called before the first frame update
    void Start()
    {
        pair1 = new Pair(m_spawnLocations[0], m_spawnLocations[1]);
        pair2 = new Pair(m_spawnLocations[0], m_spawnLocations[2]);
        pair3 = new Pair(m_spawnLocations[2], m_spawnLocations[3]);
        pair4 = new Pair(m_spawnLocations[1], m_spawnLocations[3]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isSpawning && m_enemiesLeft <= 0)
        {
            waveCount++;
            InitiateEnemySpawn(2 * waveCount, 1 * waveCount);
        }
    }

    public void InitiateEnemySpawn(int numOfEnemies, int enemiesPerSpawn)
    {
        StartCoroutine(SpawnEnemies(numOfEnemies, enemiesPerSpawn));
    }

    IEnumerator SpawnEnemies(int numOfEnemies, int enemiesPerSpawn)
    {
        m_isSpawning = true;
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
                // m_instantiatedEnemy.transform.position = m_spawnLocations[m_randomLocationIndex].position;

                // Dirty, but this is a game jam
                int randSpawnEdgeIndx = Random.Range(0, 4);
                var selecPair = (randSpawnEdgeIndx == 0) ? pair1 : (randSpawnEdgeIndx == 1) ? pair2 : (randSpawnEdgeIndx == 2) ? pair3 : pair4;
                var baseVecPair = (selecPair.transform2.position - selecPair.transform1.position).normalized;
                int randomizerEdgeVal = Random.Range(0, 100);
                var posEnemy = selecPair.transform1.position + baseVecPair * randomizerEdgeVal;

                // float dirRandX = Random.Range(0.0f, 100.0f); float dirRandZ = Random.Range(0.0f, 100.0f);
                // if (dirRandX < 50.0f) { dirRandX = -1.0f; } else { dirRandX = 1.0f; }
                // if (dirRandZ < 50.0f) { dirRandZ = -1.0f; } else { dirRandZ = 1.0f; }
                // Debug.Log("dirRandX: " + dirRandX + ", dirRandZ: " + dirRandZ);
                // float posX = 28.0f * dirRandX + Random.Range(0, 5) * dirRandX;
                // float posZ = 25.0f * dirRandZ + Random.Range(0, 5) * dirRandZ;
                // Vector3 posEnemy = new Vector3(posX, 2.3f, posZ);
                // if (isObjectHere(posEnemy)) {  } else { }

                m_instantiatedEnemy.transform.position = posEnemy;

                m_instantiatedEnemy.SetActive(true);

                enemiesSpawned++;
                m_enemiesLeft++;

                //break if enemies spawned exceeds limit
                if (enemiesSpawned >= numOfEnemies)
                {
                    break;
                }
            }
        }

        m_isSpawning = false;
    }

    public void DecreaseEnemyCount()
    {
        m_enemiesLeft--;
    }

    bool isObjectHere(Vector3 position)
    {
        Collider[] intersecting = Physics.OverlapSphere(position, 0.01f);
        if (intersecting.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
