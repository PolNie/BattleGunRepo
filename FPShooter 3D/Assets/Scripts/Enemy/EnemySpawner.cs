using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float timeToSpawn;
    public int maxSpawnEnemies;

    private float spawnCounter;
    private int spawnedEnemiesCount;

    public EnemySpawner nextSpawner;
    public int killedEnemiesThreshold;

    void Start()
    {
        spawnCounter = timeToSpawn;
        spawnedEnemiesCount = 0;

        if (nextSpawner != null)
        {
            nextSpawner.enabled = false;
        }
    }

    void Update()
    {
        if (spawnedEnemiesCount < maxSpawnEnemies)
        {
            spawnCounter -= Time.deltaTime;

            if (spawnCounter <= 0)
            {
                spawnCounter = timeToSpawn;

                Instantiate(enemyToSpawn, transform.position, transform.rotation);

                spawnedEnemiesCount++;
            }
        }

        if (nextSpawner != null && !nextSpawner.enabled && GameManager.instance.killedEnemies >= killedEnemiesThreshold)
        {
            nextSpawner.gameObject.SetActive(true);
            nextSpawner.enabled = true;
            //Debug.Log("Next spawner enabled.");
        }
    }
}
