using UnityEngine;
using System.Collections;
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

    public float positionCheckInterval = 3f;
    private float currentPositionTimer = 0f;

    private float nextInstantiationTime = 0f;

    private bool canMove = false;

    private void Awake()
    {
        target = FindObjectOfType<MageBehaviour>().transform;
        monsterScript = GetComponent<Monster>();
    }

    private IEnumerator Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = enemyStats.speed;

        yield return new WaitForSeconds(monsterScript.delayStart);
        SetRandomTargetPosition();
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            MoveToTargetPosition();

            if (Time.time >= nextInstantiationTime)
            {
                InstantiateProjectileTowardsTarget();
                nextInstantiationTime = Time.time + instantiationDelay;
            }

            currentPositionTimer += Time.deltaTime;
            if (currentPositionTimer >= positionCheckInterval)
            {
                currentPositionTimer = 0f;
                CheckIfStuck();
            }
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

    private void CheckIfStuck()
    {
        if (Vector2.Distance(transform.position, currentTargetPosition) <= 0.1f)
        {
            navMeshAgent.SetDestination(null);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        MageBehaviour mageBehaviour = other.GetComponent<MageBehaviour>();
        if (mageBehaviour != null)
        {
            mageBehaviour.TakeDamage(enemyStats.damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, movementRadius);
    }
}
