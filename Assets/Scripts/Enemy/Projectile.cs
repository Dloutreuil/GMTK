using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 target;
    private bool reachedTarget = false;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private int projectileDamage = 5;
    [SerializeField] private bool canMove;
    [SerializeField] private float destroyThreshold = 3f;

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
