using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monster))]
public class EnemyFour : MonoBehaviour
{
    public EnemyStats enemyStats;
    private Monster monsterScript;
    
    private Transform mageTransform;

    public GameObject enemyToSpawn;

    private void Awake()
    {
        monsterScript = GetComponent<Monster>();
    }

    private void Start()
    {
        mageTransform = FindObjectOfType<MageBehaviour>().transform;
    }

    private void Update()
    {
        Vector2 direction = mageTransform.position - transform.position;
        transform.Translate(direction.normalized * enemyStats.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MageBehaviour mageBehaviour = other.GetComponent<MageBehaviour>();
        if (mageBehaviour != null)
        {
            mageBehaviour.TakeDamage(enemyStats.damage);
        }
    }

    private void OnDestroy()
    {
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity); 
    }
}
