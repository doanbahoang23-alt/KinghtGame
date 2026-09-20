using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private WaveData[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool spawnOnStart = true;
    [SerializeField] private bool spawnOnTrigger = false;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        if (spawnOnStart && !spawnOnTrigger)
        {
            StartNextWave();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spawnOnTrigger && collision.CompareTag("Player") && !isSpawning)
        {
            StartNextWave();
        }
    }

    public void StartNextWave()
    {
        if (currentWaveIndex < waves.Length && !isSpawning)
        {
            StartCoroutine(SpawnWaveRoutine(waves[currentWaveIndex]));
        }
    }

    private IEnumerator SpawnWaveRoutine(WaveData wave)
    {
        isSpawning = true;

        foreach (var group in wave.enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject enemy = Instantiate(group.enemyPrefab, spawnPoint.position, Quaternion.identity);

                if (enemy.TryGetComponent(out BaseHealth health))
                {
                    health.OnDeath += HandleEnemyDeath;
                }

                activeEnemies.Add(enemy);
                yield return new WaitForSeconds(group.spawnDelay);
            }
            yield return new WaitForSeconds(wave.timeBetweenGroups);
        }

        currentWaveIndex++;
        isSpawning = false;
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}