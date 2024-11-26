using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public int initialSpawnCount = 5;
    public float spawnDistance = 10f;
    public float spawnInterval = 10f;
    public int maxEnemiesEarly = 10;
    public int maxEnemiesLate = 20;
    public float phaseChangeTime = 120f; // 2 minutes

    private List<GameObject> enemyPool = new List<GameObject>();
    private int activeEnemies = 0;
    private bool isLatePhase = false;
    
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerHeatlh>().gameObject.transform;
    }
    void Start()
    {
        // Initialize the enemy pool
        for (int i = 0; i < maxEnemiesLate; i++)
        {
             
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }

        // Spawn initial enemies
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnEnemy();
        }

        // Start spawning enemies at intervals
        StartCoroutine(SpawnEnemiesOverTime());

        // Start phase change timer
        StartCoroutine(ChangePhaseAfterTime());
    }

    void SpawnEnemy()
    {
        if (activeEnemies < (isLatePhase ? maxEnemiesLate : maxEnemiesEarly))
        {
            foreach (GameObject enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    Vector3 spawnPosition = player.position + (Vector3)(Random.insideUnitCircle.normalized * spawnDistance);
                    enemy.transform.position = spawnPosition;
                    enemy.SetActive(true);
                    activeEnemies++;
                    break;
                }
            }
        }
    }

    IEnumerator SpawnEnemiesOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int enemiesToSpawn = isLatePhase ? 2 : 1;
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
            }
        }
    }

    IEnumerator ChangePhaseAfterTime()
    {
        yield return new WaitForSeconds(phaseChangeTime);
        isLatePhase = true;
    }

    // public void OnEnemyDeactivated(GameObject enemy)
    // {
    //     enemy.SetActive(false);
    //     activeEnemies--;
    // }
}
