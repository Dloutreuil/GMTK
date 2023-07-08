using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Kickback Spell")]

public class KickbackSpell : Spell
{
    public float knockbackForce = 10f;
    public float knockbackRadius = 5f;
    public override void Activate(GameObject parent)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(parent.transform.position, knockbackRadius);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Disable the NavMeshAgent
                NavMeshAgent navMeshAgent = collider.GetComponent<NavMeshAgent>();
                if (navMeshAgent != null)
                {
                    navMeshAgent.enabled = false;
                }

                // Apply knockback force
                Rigidbody2D enemyRigidbody = collider.GetComponent<Rigidbody2D>();
                if (enemyRigidbody != null)
                {
                    Vector2 knockbackDirection = (enemyRigidbody.transform.position - parent.transform.position).normalized;
                    enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }

                // Re-enable the NavMeshAgent after a short delay
                float delay = 0.5f; // Adjust this value as needed
                EnemyManager.Instance.StartCoroutine(EnableNavMeshAgentCoroutine(navMeshAgent, delay));
            }
        }

        Debug.Log("forced");
    }

    private IEnumerator EnableNavMeshAgentCoroutine(NavMeshAgent navMeshAgent, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
