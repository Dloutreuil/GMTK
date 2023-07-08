using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool canTakeDamage = true;
    public bool isEnemyFour = false;
    [HideInInspector] public GameObject enemyToSpawn;
    public void Kill()
    {
        if (canTakeDamage)
        {
            print("I have been killed");
            Destroy(gameObject);

            if (!isEnemyFour)
            {
                SpellManager.Instance.DropSpell(transform.position);
            }
        }
        if (isEnemyFour)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }
}
