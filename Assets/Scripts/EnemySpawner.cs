using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Path path;

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.GetComponent<EnemyMovement>().InitPath(path.waypoints);
    }
}