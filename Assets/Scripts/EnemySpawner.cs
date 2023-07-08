using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyPrefab
{
    public GameObject prefab;
    public float spawnRate;
    public float cooldownTime;
    [HideInInspector] public float lastSpawnTime;
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyPrefab[] enemyPrefabs;
    public Transform[] spawnPoints;
    public NavMeshPlus.Components.NavMeshSurface navMeshSurface;

    private void Awake()
    {
        navMeshSurface = FindObjectOfType<NavMeshPlus.Components.NavMeshSurface>();
    }

    private void Start()
    {
        navMeshSurface.BuildNavMesh();
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            EnemyPrefab prefabToSpawn = GetRandomPrefab();
            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn.prefab, spawnPoint.position, spawnPoint.rotation);
                prefabToSpawn.lastSpawnTime = Time.time;
            }
        }
    }

    private EnemyPrefab GetRandomPrefab()
    {
        float totalSpawnRate = 0f;
        foreach (EnemyPrefab prefab in enemyPrefabs)
        {
            totalSpawnRate += prefab.spawnRate;
        }

        float randomValue = Random.Range(0f, totalSpawnRate);
        float cumulativeRate = 0f;

        foreach (EnemyPrefab prefab in enemyPrefabs)
        {
            if (Time.time - prefab.lastSpawnTime >= prefab.cooldownTime)
            {
                cumulativeRate += prefab.spawnRate;
                if (randomValue <= cumulativeRate)
                {
                    return prefab;
                }
            }
        }

        // If all prefabs are on cooldown, return null
        return null;
    }
}
