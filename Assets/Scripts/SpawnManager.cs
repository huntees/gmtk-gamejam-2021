using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] GameObject[] m_enemyPrefabs;
    private int m_randomEnemyIndex = 0;
    private GameObject m_instantiatedEnemy;

    [Header("Parts Prefabs")]
    [SerializeField] GameObject[] m_partPrefabs;
    private int m_randomPartIndex = 0;
    private GameObject m_instantiatedPart;


    [Header("Enemy Spawn Locations")]
    [SerializeField] private Transform[] m_spawnLocations;

    // [SerializeField] private Transform[] m_spawnRectanglesEdges;

    private int m_randomLocationIndex;

    [Header("Enemy Spawn Properties")]
    [SerializeField] private float m_maxSpawnEnemyTime = 8f;
    [SerializeField] private float m_minSpawnEnemyTime = 4f;

    [Header("Part Spawn Properties")]
    [SerializeField] private float m_maxSpawnPartTime = 8f;
    [SerializeField] private float m_minSpawnPartTime = 4f;

    private float m_randomSpawnTime = 4f;


    private int m_enemiesLeft = 0;
    private bool m_isSpawningEnemies = false;
    private bool m_isSpawningParts = false;

    private int waveCount = 0;

    private int m_enemiesPerSpawn = 1;


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
        if (!m_isSpawningEnemies && m_enemiesLeft <= 0)
        {
            waveCount++;
            if(waveCount % 3 == 0)
            {
                m_enemiesPerSpawn++;
            }

            InitiateEnemySpawn(2 * waveCount, m_enemiesPerSpawn);
        }
        if (!m_isSpawningParts)
        {
            InitiatePartSpawn(5, 1 * waveCount);
        }
    }

    public void InitiateEnemySpawn(int numOfEnemies, int enemiesPerSpawn)
    {
        StartCoroutine(SpawnEnemies(numOfEnemies, enemiesPerSpawn));
    }


    public void InitiatePartSpawn(int numOfParts, int partsPerSpawn)
    {
        StartCoroutine(SpawnParts(numOfParts, partsPerSpawn));
    }

    IEnumerator SpawnEnemies(int numOfEnemies, int enemiesPerSpawn)
    {
        m_isSpawningEnemies = true;
        int enemiesSpawned = 0;

        while (enemiesSpawned < numOfEnemies)
        {
            m_randomSpawnTime = Random.Range(m_minSpawnEnemyTime, m_maxSpawnEnemyTime);

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
                // This doesn't work and I'm not sure I get why. Anyway, we need some verification of the position not overlapping with an existing object, otherwise we get in a softlock
                // float randomizerEdgeVal = Random.Range(0.0f, baseVecPair.magnitude);
                int randSpawnEdgeIndx = Random.Range(0, 4);
                var selecPair = (randSpawnEdgeIndx == 0) ? pair1 : (randSpawnEdgeIndx == 1) ? pair2 : (randSpawnEdgeIndx == 2) ? pair3 : pair4;
                var baseVecPair = (selecPair.transform2.position - selecPair.transform1.position).normalized;
                int randomizerEdgeVal = Random.Range(0, 100);
                var posEnemy = selecPair.transform1.position + baseVecPair * randomizerEdgeVal;

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

        m_isSpawningEnemies = false;
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

    IEnumerator SpawnParts(int numOfParts, int partsPerSpawn)
    {
        m_isSpawningParts = true;
        int partsSpawned = 0;

        while (partsSpawned < numOfParts)
        {
            m_randomSpawnTime = Random.Range(m_minSpawnPartTime, m_maxSpawnPartTime);

            yield return new WaitForSeconds(m_randomSpawnTime);

            for (int i = 0; i < partsPerSpawn; i++)
            {
                //Randomise spawn location and enemy type
                m_randomLocationIndex = Random.Range(0, m_spawnLocations.Length);
                m_randomPartIndex = Random.Range(0, m_partPrefabs.Length);

                //Get any enemy and set their location
                m_instantiatedPart = Instantiate(m_partPrefabs[m_randomPartIndex], transform.position, transform.rotation);
                // m_instantiatedEnemy.transform.position = m_spawnLocations[m_randomLocationIndex].position;

                // Dirty, but this is a game jam
                int randSpawnEdgeIndx = Random.Range(0, 4);
                var selecPair = (randSpawnEdgeIndx == 0) ? pair1 : (randSpawnEdgeIndx == 1) ? pair2 : (randSpawnEdgeIndx == 2) ? pair3 : pair4;
                var baseVecPair = (selecPair.transform2.position - selecPair.transform1.position).normalized;
                int randomizerEdgeVal = Random.Range(0, 100);
                var posPart = selecPair.transform1.position + baseVecPair * randomizerEdgeVal;

                m_instantiatedPart.transform.position = posPart;
                m_instantiatedPart.SetActive(true);

                partsSpawned++;

                //break if enemies spawned exceeds limit
                if (partsSpawned >= numOfParts)
                {
                    break;
                }
            }
        }

        m_isSpawningParts = false;
    }


}
