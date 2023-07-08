using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Monster))]
public class EnemyThree : MonoBehaviour
{
    public EnemyStats enemyStats;
    private NavMeshAgent navMeshAgent;
    private Monster monsterScript;
    private Transform mageTransform;
    public float toggleDelay = 3f;

    private void Awake()
    {
        monsterScript = GetComponent<Monster>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        mageTransform = FindObjectOfType<MageBehaviour>().transform;
        StartCoroutine(ToggleCanTakeDamage());
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

    private IEnumerator ToggleCanTakeDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(toggleDelay);
            if (monsterScript.canTakeDamage)
                monsterScript.canTakeDamage = false;
            else
            {
                monsterScript.canTakeDamage = true;

            }
        }
    }
}
