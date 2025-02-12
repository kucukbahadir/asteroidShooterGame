using System.Collections;
using System.Collections.Generic; // Ensure this is included for Queue<T>
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs; // Prefab list
    [SerializeField] private int maxAsteroids = 5; // Max asteroids allowed at the same time
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private float maxSpawnDelay = 3f;
    [SerializeField] private float minLifetime = 4f;
    [SerializeField] private float maxLifetime = 8f;

    public bool stopSpawning = false; // Toggle for stopping/resuming asteroid spawning

    private int currentAsteroids = 0; // Track active asteroids
    private Queue<GameObject> asteroidPool; // Declare the queue

    private void Start()
    {
        // Initialize the pool properly inside Start()
        asteroidPool = new Queue<GameObject>();

        // Fill the pool with inactive asteroids
        for (int i = 0; i < maxAsteroids; i++)
        {
            GameObject asteroid = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)]);
            asteroid.SetActive(false);
            asteroidPool.Enqueue(asteroid);
        }

        // Start spawning coroutines
        for (int i = 0; i < maxAsteroids; i++)
        {
            StartCoroutine(SpawnAsteroidRoutine());
        }
    }

    private IEnumerator SpawnAsteroidRoutine()
    {
        while (true)
        {
            while (stopSpawning)
            {
                yield return null;
            }

            if (currentAsteroids < maxAsteroids && asteroidPool.Count > 0)
            {
                SpawnAsteroid();
            }

            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPool.Count == 0) return; // Prevent errors if the pool is empty

        GameObject asteroid = asteroidPool.Dequeue();
        asteroid.SetActive(true);

        // Set random spawn position
        Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
        asteroid.transform.position = spawnPosition;

        currentAsteroids++;
        StartCoroutine(MoveAsteroid(asteroid));
    }

    private IEnumerator MoveAsteroid(GameObject asteroid)
    {
        Vector3 moveDirection = Vector3.forward;
        float lifetime = Random.Range(minLifetime, maxLifetime);
        float moveSpeed = Random.Range(10f, 5f);

        float timer = 0f;
        while (timer < lifetime)
        {
            asteroid.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        asteroid.SetActive(false); // Return to pool
        asteroidPool.Enqueue(asteroid);
        currentAsteroids--;
    }
}
