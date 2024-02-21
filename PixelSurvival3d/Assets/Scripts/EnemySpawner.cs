using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the enemy prefab in the Inspector
    public float spawnInterval = 5f; // Time between each spawn
    public int maxEnemies = 10; // Maximum number of enemies at once
    public float spawnDistance = 50f; // Distance from the player to spawn enemies
    public Transform player; // Assign the player's transform in the Inspector

    private float timeSinceLastSpawn;
    private int currentEnemyCount;

    private void Start()
    {
        timeSinceLastSpawn = 0f;
        currentEnemyCount = 0;
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval && currentEnemyCount < maxEnemies)
        {
            SpawnEnemyNearPlayer();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemyNearPlayer()
    {
        // Generate a random position around the player within spawnDistance
        Vector3 spawnPosition = player.position + Random.insideUnitSphere * spawnDistance;
        // Ensure the spawn position is on the ground. Adjust this line if your game uses a different method to determine the ground level.
        spawnPosition.y = 0; // Set this according to your game's ground level or use a method to find the ground

        // Instantiate the enemy at the spawn position
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;

        // Optional: If your enemies need to perform any initialization logic knowing they were spawned by this spawner, do it here
        // Example: spawnedEnemy.GetComponent<Enemy>().Initialize(spawner: this);
    }

    // Call this method when an enemy dies to decrease the enemy count
    public void OnEnemyDeath()
    {
        if (currentEnemyCount > 0)
        {
            currentEnemyCount--;
        }
    }
}
