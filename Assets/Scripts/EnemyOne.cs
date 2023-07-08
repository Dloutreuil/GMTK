using UnityEngine;
using System.Collections;
using UnityEngine.AI;
[RequireComponent(typeof(Monster))]

public class EnemyOne : MonoBehaviour
{
    public EnemyStats enemyStats;
    private Monster monsterScript;
    public NavMeshAgent navMeshAgent;
    private Transform mageTransform;

    private bool canMove = false;
    private void Awake()
    {
        monsterScript = GetComponent<Monster>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        if (navMeshAgent != null)
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
}
