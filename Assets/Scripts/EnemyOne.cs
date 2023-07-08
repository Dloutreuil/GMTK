using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Monster))]

public class EnemyOne : MonoBehaviour
{
    public EnemyStats enemyStats;
    private Monster monsterScript;
    public NavMeshAgent navMeshAgent;
    private Transform mageTransform;

    private void Awake()
    {
        monsterScript = GetComponent<Monster>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        mageTransform = FindObjectOfType<MageBehaviour>().transform;
        navMeshAgent.speed = enemyStats.speed;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
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
}
