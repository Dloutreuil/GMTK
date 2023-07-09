using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArenaManager : MonoBehaviour
{
    [Serializable]
    public struct ArenaPair
    {
        public GameObject spawnableArena;
        public GameObject preSpawnableArena;
    }

    public List<ArenaPair> arenaPairs;

    public float spawnInterval = 5f;  // Time interval between spawns
    public float blinkDurationRemoveArena = 5f;
    public float preSpawnBlinkDuration = 2f;  // Duration of preSpawnableArena blinking
    public float preSpawnBlinkInterval = 2f;  // Duration of preSpawnableArena blinking
    public float timeToHoldArena = 10f;

    private GameObject currentSpawnedArena;
    private GameObject lastSpawnedArena;
    public GameObject grid;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        // Start spawning arenas
        spawnCoroutine = StartCoroutine(SpawnArenas());
    }

    private IEnumerator SpawnArenas()
    {
        yield return new WaitForSeconds(spawnInterval);
        while (true)
        {
            // Select a random arena to spawn
            ArenaPair arenaPairToSpawn = GetRandomArenaPair();

            // Check if the arena to spawn is the same as the last spawned arena
            if (arenaPairToSpawn.spawnableArena == lastSpawnedArena)
            {
                // If it is, get a new random arena
                arenaPairToSpawn = GetRandomArenaPair();
            }

            // Check if the arena to spawn is the same as the current spawned arena
            if (arenaPairToSpawn.spawnableArena == currentSpawnedArena)
            {
                // If it is, wait for the next spawn interval and continue to the next iteration
                break;
            }

            // Instantiate the preSpawnableArena
            GameObject preSpawnableArena = Instantiate(arenaPairToSpawn.preSpawnableArena, transform.position, Quaternion.identity, grid.transform);

            // Start blinking the preSpawnableArena
            Coroutine preSpawnBlinkCoroutine = StartCoroutine(BlinkPreSpawnableArena(preSpawnableArena));

            // Wait until the blinking is done
            yield return new WaitForSeconds(preSpawnBlinkDuration);

            // Stop blinking the preSpawnableArena
            StopCoroutine(preSpawnBlinkCoroutine);
            preSpawnableArena.SetActive(false);

            // Instantiate the spawnableArena
            GameObject spawnedArena = Instantiate(arenaPairToSpawn.spawnableArena, transform.position, Quaternion.identity, grid.transform);

            // Update the current and last spawned arenas
            currentSpawnedArena = spawnedArena;
            lastSpawnedArena = spawnedArena;

            NavMeshBaker.Instance.BakeNavMesh();
            yield return new WaitForSeconds(timeToHoldArena);


            // Blink the spawnedArena for the specified duration while keeping its collider active
            Coroutine blinkCoroutine = StartCoroutine(BlinkArenaWithCollider(spawnedArena, blinkDurationRemoveArena));

            // Wait for the specified blink duration
            yield return new WaitForSeconds(blinkDurationRemoveArena);

            // Stop blinking the spawnedArena
            StopCoroutine(blinkCoroutine);

            // Destroy the spawnedArena
            Destroy(spawnedArena);

            // Destroy the preSpawnableArena
            Destroy(preSpawnableArena);

            
        }
    }




    private ArenaPair GetRandomArenaPair()
    {
        // Create a list of available arena pairs
        List<ArenaPair> availableArenaPairs = new List<ArenaPair>();

        foreach (var arenaPair in arenaPairs)
        {
            // Exclude the current spawned arena and the last spawned arena pairs
            if (arenaPair.spawnableArena != currentSpawnedArena && arenaPair.spawnableArena != lastSpawnedArena)
            {
                availableArenaPairs.Add(arenaPair);
            }
        }

        // Select a random arena pair from the available arena pairs
        int randomIndex = UnityEngine.Random.Range(0, availableArenaPairs.Count);
        return availableArenaPairs[randomIndex];
    }

    private IEnumerator BlinkPreSpawnableArena(GameObject preSpawnableArena)
    {
        bool isBlinking = true;

        while (isBlinking)
        {
            // Toggle the visibility of the preSpawnableArena
            preSpawnableArena.SetActive(!preSpawnableArena.activeSelf);

            // Wait for the specified blink interval
            yield return new WaitForSeconds(preSpawnBlinkInterval);
        }

        // Destroy the preSpawnableArena
        Destroy(preSpawnableArena);
    }

    private IEnumerator BlinkArenaWithCollider(GameObject arena, float blinkDuration)
    {
        float elapsedTime = 0f;
        bool isBlinking = true;

        // Get all PolygonCollider2D components in the arena's children
        PolygonCollider2D[] arenaColliders = arena.GetComponentsInChildren<PolygonCollider2D>();

        while (isBlinking)
        {
            // Toggle the visibility of the arena
            arena.SetActive(!arena.activeSelf);

            // Enable or disable PolygonCollider2D components on all child objects
            foreach (PolygonCollider2D collider in arenaColliders)
            {
                collider.enabled = arena.activeSelf;
            }

            elapsedTime += Time.deltaTime;

            // Check if the blink duration has elapsed
            if (elapsedTime >= blinkDuration)
            {
                isBlinking = false;
            }

            // Wait for the next frame
            yield return new WaitForSeconds(preSpawnBlinkInterval);
        }

        // Disable PolygonCollider2D components on all child objects
        foreach (PolygonCollider2D collider in arenaColliders)
        {
            collider.enabled = false;
        }
    }
}
