using System.Collections;
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

    private void Start()
    {
        // Start multiple coroutines to ensure multiple asteroids spawn over time
        for (int i = 0; i < maxAsteroids; i++)
        {
            StartCoroutine(SpawnAsteroidRoutine());
        }
    }

    private IEnumerator SpawnAsteroidRoutine()
    {
        while (true)
        {
            // Wait while spawning is paused
            while (stopSpawning)
            {
                yield return null;
            }

            if (currentAsteroids < maxAsteroids)
            {
                SpawnAsteroid();
            }

            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefabs.Length == 0) return; // Prevent errors

        // Pick a random prefab
        GameObject prefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Instantiate at a random position around this object
        Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
        GameObject asteroid = Instantiate(prefab, spawnPosition, Quaternion.identity);
        currentAsteroids++;

        // Start moving the asteroid
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

        Destroy(asteroid);
        currentAsteroids--;
    }
}
