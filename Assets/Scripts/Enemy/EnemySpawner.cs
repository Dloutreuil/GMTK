using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class EnemyPrefab
{
    public GameObject prefab;
    public float spawnRate = 1f;
    public float cooldown = 3f;
    [HideInInspector] public float lastSpawnTime;
    [HideInInspector] public bool isSpawning;

}

public class EnemySpawner : MonoBehaviour
{
    public EnemyPrefab[] enemyPrefabs;
    public float spawnerCooldown = 5f;

    private NavMeshSurface navMeshSurface;
    private float currentSpawnerCooldown;
    private List<EnemyPrefab> availablePrefabs;

    private void Awake()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
        currentSpawnerCooldown = spawnerCooldown;
        InitializeAvailablePrefabs();
    }

    private void Start()
    {
        navMeshSurface.BuildNavMesh();
    }

    private void Update()
    {
        if (currentSpawnerCooldown <= 0f)
        {
            SpawnEnemy();
            currentSpawnerCooldown = spawnerCooldown;
        }
        else
        {
            currentSpawnerCooldown -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        EnemyPrefab prefabToSpawn = GetRandomPrefab();
        if (prefabToSpawn != null && CanSpawnEnemy(prefabToSpawn))
        {
            StartCoroutine(SpawnEnemyCoroutine(prefabToSpawn));
            prefabToSpawn.lastSpawnTime = Time.time;
        }
    }

    private IEnumerator SpawnEnemyCoroutine(EnemyPrefab enemyPrefab)
    {
        enemyPrefab.isSpawning = true;

        int maxAttempts = 20;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();

            // Create a preview sprite at the random position
            GameObject previewSprite = Instantiate(enemyPrefab.prefab, randomPosition, Quaternion.identity, gameObject.transform);
            Renderer spriteRenderer = previewSprite.GetComponentInChildren<Renderer>();
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.5f); // Set the preview sprite's color and transparency

            // Start the coroutine to destroy the preview sprite after a couple of seconds
            StartCoroutine(DestroyPreviewSprite(previewSprite));

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 1f, NavMesh.AllAreas))
            {
                // Update the position of the preview sprite to the valid hit position
                previewSprite.transform.position = hit.position;

                // Spawn the enemy at the valid position after a delay
                float spawnDelay = 2f;
                yield return new WaitForSeconds(spawnDelay);
                GameObject instantiatedEnemy = Instantiate(enemyPrefab.prefab, hit.position, Quaternion.identity, gameObject.transform);
                instantiatedEnemy.transform.position = hit.position;

                enemyPrefab.lastSpawnTime = Time.time; // Update last spawn time for the spawned prefab
                currentSpawnerCooldown = spawnerCooldown; // Reset spawner cooldown

                if (enemyPrefab.cooldown <= 0f)
                {
                    availablePrefabs.Add(enemyPrefab); // Re-add spawned prefab to available prefabs list
                }

                enemyPrefab.isSpawning = false;
                yield break;
            }

            // Destroy the preview sprite if it's not on the NavMesh
            Destroy(previewSprite);
            Debug.LogWarning("Failed to spawn enemy on NavMesh. Attempting to find a new position.");

            attempts++;
        }

        Debug.LogError("Failed to find a valid position to spawn enemy on NavMesh after multiple attempts.");
        enemyPrefab.isSpawning = false;
    }



    private void InitializeAvailablePrefabs()
    {
        availablePrefabs = new List<EnemyPrefab>(enemyPrefabs);
    }

    private EnemyPrefab GetRandomPrefab()
    {
        List<EnemyPrefab> validPrefabs = GetValidPrefabs();
        if (validPrefabs.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, validPrefabs.Count);
        return validPrefabs[randomIndex];
    }

    private List<EnemyPrefab> GetValidPrefabs()
    {
        List<EnemyPrefab> validPrefabs = new List<EnemyPrefab>();
        foreach (EnemyPrefab prefab in availablePrefabs)
        {
            if (CanSpawnEnemy(prefab))
            {
                validPrefabs.Add(prefab);
            }
        }
        return validPrefabs;
    }

    private bool CanSpawnEnemy(EnemyPrefab enemyPrefab)
    {
        float elapsedTime = Time.time - enemyPrefab.lastSpawnTime;
        return elapsedTime >= enemyPrefab.cooldown && Random.value <= enemyPrefab.spawnRate;
    }

    /*private void SpawnEnemy(EnemyPrefab enemyPrefab)
    {
        int maxAttempts = 10;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();

            // Create a preview sprite at the random position
            GameObject previewSprite = Instantiate(enemyPrefab.prefab, randomPosition, Quaternion.identity, gameObject.transform);
            Renderer spriteRenderer = previewSprite.GetComponentInChildren<Renderer>();
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.5f); // Set the preview sprite's color and transparency

            // Start the coroutine to destroy the preview sprite after a couple of seconds
            StartCoroutine(DestroyPreviewSprite(previewSprite));

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPosition, out hit, 1f, NavMesh.AllAreas))
            {
                // Update the position of the preview sprite to the valid hit position
                previewSprite.transform.position = hit.position;

                // Spawn the enemy at the valid position after a delay
                float spawnDelay = 2f;
                StartCoroutine(SpawnEnemyAfterDelay(enemyPrefab, hit.position, spawnDelay));

                return;
            }

            // Destroy the preview sprite if it's not on the NavMesh
            Destroy(previewSprite);
            Debug.LogWarning("Failed to spawn enemy on NavMesh. Attempting to find a new position.");

            attempts++;
        }

        Debug.LogError("Failed to find a valid position to spawn enemy on NavMesh after multiple attempts.");
    }*/

    private IEnumerator DestroyPreviewSprite(GameObject previewSprite)
    {
        float previewDuration = 2f;
        yield return new WaitForSeconds(previewDuration);
        Destroy(previewSprite);
    }

   /* private IEnumerator SpawnEnemyAfterDelay(EnemyPrefab enemyPrefab, Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Spawn the enemy at the valid position
        GameObject instantiatedEnemy = Instantiate(enemyPrefab.prefab, position, Quaternion.identity, gameObject.transform);
        instantiatedEnemy.transform.position = position;

        enemyPrefab.lastSpawnTime = Time.time; // Update last spawn time for the spawned prefab
        currentSpawnerCooldown = spawnerCooldown; // Reset spawner cooldown

        if (enemyPrefab.cooldown <= 0f)
        {
            availablePrefabs.Add(enemyPrefab); // Re-add spawned prefab to available prefabs list
        }
    }*/

    private Vector3 GetRandomSpawnPosition()
    {
        Bounds bounds = navMeshSurface.navMeshData.sourceBounds;
        Vector3 randomPoint = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
        return randomPoint;
    }
}
