using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    public static EnemyManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void RestartAI()
    {
        NavMeshAgent[] navMeshAgents = GetComponentsInChildren<NavMeshAgent>();
        Rigidbody2D[] rigidbodies = GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D rbs in rigidbodies)
        {
            rbs.bodyType = RigidbodyType2D.Static;
        }

        foreach (NavMeshAgent navMeshAgent in navMeshAgents)
        {
            navMeshAgent.enabled = true;
        }

    }

}
