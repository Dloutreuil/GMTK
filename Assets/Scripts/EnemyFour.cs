using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Monster))]
public class EnemyFour : MonoBehaviour
{
    public EnemyStats enemyStats;
    private Monster monsterScript;
    public NavMeshAgent navMeshAgent;

    private Transform mageTransform;

    public GameObject enemyToSpawn;

    private void Awake()
    {
        monsterScript = GetComponent<Monster>();
    }

    private void Start()
    {
        mageTransform = FindObjectOfType<MageBehaviour>().transform;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = enemyStats.speed;
    }

    private void Update()
    {
        MoveTowardsMage();
    }
    private void MoveTowardsMage()
    {
        navMeshAgent.SetDestination(mageTransform.position);
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
