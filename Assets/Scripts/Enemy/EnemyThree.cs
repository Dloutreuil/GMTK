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
    private bool canMove = false;

    private Color savedColor;
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
        yield return new WaitForSeconds(monsterScript.delayStart);

        StartCoroutine(ToggleCanTakeDamage());
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
        {
            navMeshAgent.SetDestination(mageTransform.position);
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

    private IEnumerator ToggleCanTakeDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(toggleDelay);
            if (monsterScript.canTakeDamage)
            {
                Renderer spriteRenderer = gameObject.GetComponentInChildren<Renderer>();
                savedColor = spriteRenderer.material.color;
                spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.5f);
                monsterScript.canTakeDamage = false;

            }
            else
            {
                Renderer spriteRenderer = gameObject.GetComponentInChildren<Renderer>();
                spriteRenderer.material.color = savedColor;
                monsterScript.canTakeDamage = true;

            }
        }
    }
}
