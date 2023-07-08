using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Monster))]
public class EnemyTwo : MonoBehaviour
{
    public EnemyStats enemyStats;
    public GameObject projectilePrefab;
    public NavMeshAgent navMeshAgent;
    private Monster monsterScript;

    private Transform target;
    private Vector2 currentTargetPosition;

    public float instantiationDelay = 3f;
    public float movementRadius = 5f;

    private float nextInstantiationTime = 0f;
    private void Awake()
    {
        target = FindObjectOfType<MageBehaviour>().transform;
        monsterScript = GetComponent<Monster>();

    }

    private void Start()
    {
        SetRandomTargetPosition();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = enemyStats.speed;
    }

    private void Update()
    {
        MoveToTargetPosition();

        if (Time.time >= nextInstantiationTime)
        {
            InstantiateProjectileTowardsTarget();
            nextInstantiationTime = Time.time + instantiationDelay;
        }
    }

    private void MoveToTargetPosition()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        Vector2 randomPosition = Random.insideUnitCircle.normalized * movementRadius;
        currentTargetPosition = randomPosition;
        navMeshAgent.SetDestination(currentTargetPosition);
    }

    private void InstantiateProjectileTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

        GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.SetTarget(target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, movementRadius);
    }
}
