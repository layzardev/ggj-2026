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
    public float spawnDelay = 3f;
    public int enemiesPerWave = 1;

    [Header("Player Check")]
    public Transform player;
    public float minSpawnDistanceFromPlayer = 10f;

    private Coroutine spawnRoutine;
    private int levelUpCount;

    private void Start()
    {
        StartSpawner();
    }

    private void StartSpawner()
    {
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(spawnDelay);

        SpawnEnemies();

        // Coroutine calls itself
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = GetValidSpawnPoint();
            if (spawnPoint == null) 
                return;
            Vector3 spawnPosition = GetGroundedPosition(spawnPoint.position);

            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private Vector3 GetGroundedPosition(Vector3 spawnPos)
    {
        Ray ray = new Ray(spawnPos + Vector3.up * 1f, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 2f, LayerMask.GetMask("Ground")))
        {
            return hit.point;
        }

        return spawnPos;
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

        if (levelUpCount >= 4)
        {
            levelUpCount = 0;
            RestartSpawnerWithUpdatedValues();
        }
    }

    private void RestartSpawnerWithUpdatedValues()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        // Example scaling – tweak freely
        spawnDelay = Mathf.Max(0.5f, spawnDelay - 0.3f);
        enemiesPerWave++;

        StartSpawner();
    }
}
