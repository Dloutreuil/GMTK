using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float delayStart = 3f;
    public bool canTakeDamage = true;
    public bool isEnemyFour = false;
    public GameObject enemyToSpawn;
    public GameObject dieVFX;
    public void Kill()
    {
        if (canTakeDamage)
        {
            print("I have been killed");
            Destroy(gameObject);

            if (!isEnemyFour)
            {
                GameObject vfxGO = Instantiate(dieVFX, transform.position, Quaternion.identity,null);
                Destroy(vfxGO, 10);
                SpellManager.Instance.DropSpell(transform.position);
                GameManager.Instance.ChangeScore();
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
