using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private int maxAsteroids = 16; // Allow all 16 to be active
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float minSpawnDelay = 2f; // Time between meteor storms
    [SerializeField] private float maxSpawnDelay = 5f;
    [SerializeField] private float minLifetime = 8f;
    [SerializeField] private float maxLifetime = 12f;

    public bool stopSpawning = false;

    private Queue<GameObject> asteroidPool;
    private int activeAsteroids = 0;

    private void Start()
    {
        asteroidPool = new Queue<GameObject>();

        // Create 16 asteroids in the pool
        for (int i = 0; i < maxAsteroids; i++)
        {
            GameObject asteroid = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)]);
            asteroid.SetActive(false);
            asteroidPool.Enqueue(asteroid);
        }

        // Start the spawning system
        StartCoroutine(SpawnAsteroidWaves());
    }

    private IEnumerator SpawnAsteroidWaves()
    {
        while (true)
        {
            while (stopSpawning)
            {
                yield return null;
            }

            // Spawn multiple asteroids at once (random 4-8 per wave)
            int asteroidsToSpawn = Random.Range(4, 9);

            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                if (asteroidPool.Count > 0)
                {
                    SpawnAsteroid();
                }
            }

            // Wait before spawning the next wave (2-5 seconds)
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPool.Count == 0) return;

        GameObject asteroid = asteroidPool.Dequeue();
        asteroid.SetActive(true);

        // Random spawn position around the spawner
        Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
        asteroid.transform.position = spawnPosition;

        activeAsteroids++;
        StartCoroutine(MoveAsteroid(asteroid));
    }

    private IEnumerator MoveAsteroid(GameObject asteroid)
    {
        float lifetime = Random.Range(minLifetime, maxLifetime);

        float moveSpeed = Random.Range(10f, 5f);

        float timer = 0f;
        while (timer < lifetime)
        {
            asteroid.transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        // Return asteroid to pool
        asteroid.SetActive(false);
        asteroidPool.Enqueue(asteroid);
        activeAsteroids--;
    }
}
