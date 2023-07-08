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
    private bool canMove = false;

    private void Awake()
    {
        monsterScript = GetComponent<Monster>();
    }

    private IEnumerator Start()
    {
        mageTransform = FindObjectOfType<MageBehaviour>().transform;
        navMeshAgent.speed = enemyStats.speed;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        yield return new WaitForSeconds(3);
        canMove = true;
    }
    private void Update()
    {
        if (canMove)
        {
            MoveTowardsMage();
        }
    }
    private void MoveTowardsMage()
    {
        navMeshAgent.SetDestination(mageTransform.position);
    }

    private void OnTriggerStay2D(Collider2D other)
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
