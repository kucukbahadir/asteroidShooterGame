using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs; // Prefab list
    [SerializeField] private int asteroidCount = 5; // Number of asteroids to spawn
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float respawnTime = 5f;

    private void Start()
    {
        if (asteroidPrefabs.Length == 0)
        {
            Debug.LogError("No asteroid prefabs assigned!");
            return;
        }

        for (int i = 0; i < asteroidCount; i++)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        // Pick a random prefab
        GameObject prefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Instantiate at a random position around this object
        Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
        GameObject asteroid = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // Start moving the asteroid
        StartCoroutine(MoveAsteroid(asteroid));
    }

    private IEnumerator MoveAsteroid(GameObject asteroid)
    {
        Vector3 moveDirection = Random.insideUnitCircle.normalized;
        float timer = 0f;

        while (timer < respawnTime)
        {
            asteroid.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(asteroid); // Destroy after time limit
        SpawnAsteroid(); // Respawn a new one
    }
}
