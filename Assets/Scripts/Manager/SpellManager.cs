using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableSpellData
{
    public GameObject spellPrefab;
    public float dropRate = 0.5f;
    public float destroyCooldown = 5f;
    public int maximumSpawned = 3;
}

public class SpellManager : MonoBehaviour
{
    public List<SpawnableSpellData> spawnableSpells = new List<SpawnableSpellData>();

    private Dictionary<GameObject, int> spawnedSpellCount = new Dictionary<GameObject, int>();

    private static SpellManager instance;
    public static SpellManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void DropSpell(Vector3 positionToSpawn)
    {
        // Check if any spawnable spell is available
        if (spawnableSpells.Count == 0)
        {
            Debug.LogWarning("No spawnable spells available.");
            return;
        }

        // Randomly select a spell to spawn based on drop rates
        GameObject spellToSpawn = SelectRandomSpell();
        if (spellToSpawn == null)
        {
            Debug.LogWarning("Failed to select a spell to spawn.");
            return;
        }

        // Check if the maximum number of spells has been spawned for the selected spell
        int spawnedCount = GetSpawnedSpellCount(spellToSpawn);
        if (spawnedCount >= spellToSpawn.GetComponent<SpawnableSpellData>().maximumSpawned)
        {
            Debug.LogWarning("Maximum number of spells spawned for the selected spell.");
            return;
        }

        // Instantiate the spell at the specified position
        GameObject instantiatedSpell = Instantiate(spellToSpawn, positionToSpawn, Quaternion.identity);

        // Start the destroy cooldown timer
        StartCoroutine(DestroySpellAfterCooldown(instantiatedSpell, spellToSpawn));

        // Increment the spawned count for the selected spell
        IncrementSpawnedSpellCount(spellToSpawn);
    }

    private GameObject SelectRandomSpell()
    {
        float totalDropRate = 0f;

        // Calculate the total drop rate
        foreach (var spellData in spawnableSpells)
        {
            totalDropRate += spellData.dropRate;
        }

        // Randomly select a spell based on the drop rates
        float randomValue = UnityEngine.Random.value;
        float dropRateSum = 0f;

        foreach (var spellData in spawnableSpells)
        {
            dropRateSum += spellData.dropRate / totalDropRate;

            if (randomValue <= dropRateSum)
            {
                return spellData.spellPrefab;
            }
        }

        return null;
    }

    private int GetSpawnedSpellCount(GameObject spellPrefab)
    {
        if (spawnedSpellCount.ContainsKey(spellPrefab))
        {
            return spawnedSpellCount[spellPrefab];
        }

        return 0;
    }

    private void IncrementSpawnedSpellCount(GameObject spellPrefab)
    {
        if (spawnedSpellCount.ContainsKey(spellPrefab))
        {
            spawnedSpellCount[spellPrefab]++;
        }
        else
        {
            spawnedSpellCount.Add(spellPrefab, 1);
        }
    }

    private IEnumerator DestroySpellAfterCooldown(GameObject spellObject, GameObject spellPrefab)
    {
        SpawnableSpellData spellData = spellPrefab.GetComponent<SpawnableSpellData>();

        yield return new WaitForSeconds(spellData.destroyCooldown);

        Destroy(spellObject);

        // Decrement the spawned count for the spell
        if (spawnedSpellCount.ContainsKey(spellPrefab))
        {
            spawnedSpellCount[spellPrefab]--;
        }
    }
}
