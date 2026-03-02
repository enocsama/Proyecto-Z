using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    public int enemiesPerWave = 5;
    public float timeBetweenEnemies = 1f;
    public float timeBetweenWaves = 5f;

    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(StartWaveLoop());
    }

    IEnumerator StartWaveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }
}