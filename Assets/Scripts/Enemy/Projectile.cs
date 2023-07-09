using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 target;
    private bool reachedTarget = false;
    private float timeElapsed = 0f;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private int projectileDamage = 5;
    [SerializeField] private bool canMove;
    [SerializeField] private float destroyThreshold = 3f;
    [SerializeField] private float destroyDelay = 1f;

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (!reachedTarget)
        {
            Vector3 direction = target - transform.position;
            transform.Translate(direction.normalized * projectileSpeed * Time.deltaTime, Space.World);

            // Check if reached target position
            if (Vector3.Distance(transform.position, target) <= destroyThreshold)
            {
                reachedTarget = true;
            }
        }
        else
        {
            // Continue moving in the same direction
            transform.Translate(transform.forward * projectileSpeed * Time.deltaTime, Space.World);
            timeElapsed += Time.deltaTime;

            // Check if the projectile should be destroyed
            if (timeElapsed >= destroyDelay)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mage"))
        {
            MageBehaviour mageBehaviour = other.GetComponent<MageBehaviour>();
            mageBehaviour.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
