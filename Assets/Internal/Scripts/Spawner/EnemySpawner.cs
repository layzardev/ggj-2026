using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private SpawnerContainer spawnerContainer;

    [Header("Spawn Settings")]
    public float initialSpawnDelay = 3f;
    public int initialSpawn = 2;
    public float spawnModifier = 1f;

    [Header("Player Check")]
    public Transform player;
    public float minSpawnDistanceFromPlayer = 10f;

    private Coroutine spawnRoutine;
    private int levelUpCount = 1;
    private float currentSpawnDelay;
    private int currentSpawnCount; // How many spawn at a time

    private void Start()
    {
        StartSpawner();
        currentSpawnDelay = initialSpawnDelay;
        currentSpawnCount = initialSpawn;
    }

    private void StartSpawner()
    {
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(currentSpawnDelay);

        SpawnEnemies();

        // Coroutine calls itself
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < currentSpawnCount; i++)
        {
            Transform spawnPoint = GetValidSpawnPoint();
            if (spawnPoint == null) 
                break;

            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private Transform GetValidSpawnPoint()
    {
        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            Transform marker = spawnerContainer.GetRandomMarker();
            if (marker == null) return null;

            float distance = Vector3.Distance(marker.position, player.position);
            if (distance >= minSpawnDistanceFromPlayer)
                return marker;
        }

        return null;
    }

    // 🔹 CALL THIS FROM YOUR EXISTING LEVEL UP SYSTEM
    public void OnLevelUpTriggered()
    {
        levelUpCount++;
        RestartSpawnerWithUpdatedValues();
    }

    private void RestartSpawnerWithUpdatedValues()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        UpdateWaveSettings(levelUpCount);
        StartSpawner();
    }

    private void UpdateWaveSettings(int level){
        float modifier = 1f;
        int step = (level-2) % 5; // cycle through pola 2-5
        switch(step) {
            case 0:
                spawnModifier += 0.2f;
                break;
            case 1:
                spawnModifier += 0.1f;
                break;
            case 2:
                spawnModifier += 0.2f;
                break;
            case 3:
                spawnModifier -= 0.4f;
                currentSpawnCount++;
                break;
        }
        currentSpawnDelay = initialSpawnDelay / spawnModifier;
    }
}
