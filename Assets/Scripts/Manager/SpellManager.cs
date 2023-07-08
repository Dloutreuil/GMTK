using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpawnableSpellData
{
    public GameObject spellPrefab;
    [Min(0)] public float dropRate = 0.5f;
    public float destroyCooldown = 5f;
    public int maximumSpawned = 3;
}

public class SpellManager : MonoBehaviour
{
    public List<SpawnableSpellData> spawnableSpells = new List<SpawnableSpellData>();
    public float nullSpellChance = 0.1f;
    private Dictionary<GameObject, SpawnableSpellData> spellDataMap = new Dictionary<GameObject, SpawnableSpellData>();
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

        // Build the spell data map for efficient lookup
        foreach (SpawnableSpellData spellData in spawnableSpells)
        {
            if (!spellDataMap.ContainsKey(spellData.spellPrefab))
            {
                spellDataMap.Add(spellData.spellPrefab, spellData);
            }
        }
    }

    public void DropSpell(Vector3 positionToSpawn)
    {
        // Determine if a null spell should be returned
        if (UnityEngine.Random.value <= nullSpellChance)
        {
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
        SpawnableSpellData spellData = spellDataMap[spellToSpawn];
        if (spawnedCount >= spellData.maximumSpawned)
        {
            Debug.LogWarning("Maximum number of spells spawned for the selected spell.");
            return;
        }

        // Instantiate the spell at the specified position
        GameObject instantiatedSpell = Instantiate(spellToSpawn, positionToSpawn, Quaternion.identity);

        // Start the destroy cooldown timer
        StartCoroutine(DestroySpellAfterCooldown(instantiatedSpell, spellData));
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

    private IEnumerator DestroySpellAfterCooldown(GameObject spellObject, SpawnableSpellData spellData)
    {
        float cooldownTimer = spellData.destroyCooldown;

        while (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }

        Destroy(spellObject);

        // Decrement the spawned count for the spell
        if (spawnedSpellCount.ContainsKey(spellData.spellPrefab))
        {
            spawnedSpellCount[spellData.spellPrefab]--;
        }
    }

    private void OnValidate()
    {
        /*
         // Ensure total drop rates add up to 1 (100%)
         float totalDropRates = 0f;
         foreach (SpawnableSpellData spellData in spawnableSpells)
         {
             totalDropRates += spellData.dropRate;
         }

         // Add nullSpellChance to the total drop rates
         totalDropRates += nullSpellChance;

         // Adjust drop rates if the total doesn't equal 1
         if (totalDropRates != 1f)
         {
             float scale = 1f / totalDropRates;
             foreach (SpawnableSpellData spellData in spawnableSpells)
             {
                 spellData.dropRate *= scale;
             }
             nullSpellChance *= scale;
         }

         // Ensure nullSpellChance is within the valid range
         nullSpellChance = Mathf.Clamp01(nullSpellChance);*/

        // Calculate total drop rates without nullSpellChance
        float totalDropRates = 0f;
        foreach (SpawnableSpellData spellData in spawnableSpells)
        {
            totalDropRates += spellData.dropRate;
        }

        // Calculate the remaining drop rate to reach 1
        float remainingDropRate = 1f - totalDropRates;

        // Adjust nullSpellChance if remainingDropRate is positive
        if (remainingDropRate > 0f)
        {
            nullSpellChance = Mathf.Clamp01(remainingDropRate);
        }
        else
        {
            nullSpellChance = 0f;
        }

        // Ensure nullSpellChance is within the valid range
        nullSpellChance = Mathf.Clamp01(nullSpellChance);

    }

    private void OnEnable()
    {
        // Calculate total drop rates without nullSpellChance
        float totalDropRates = 0f;
        foreach (SpawnableSpellData spellData in spawnableSpells)
        {
            totalDropRates += spellData.dropRate;
        }

        // Calculate the remaining drop rate to reach 1
        float remainingDropRate = 1f - totalDropRates;

        // Adjust nullSpellChance if remainingDropRate is positive
        if (remainingDropRate > 0f)
        {
            nullSpellChance = Mathf.Clamp01(remainingDropRate);
        }
        else
        {
            nullSpellChance = 0f;
        }

        // Ensure nullSpellChance is within the valid range
        nullSpellChance = Mathf.Clamp01(nullSpellChance);

    }

}
