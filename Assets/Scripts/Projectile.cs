using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private int projectileDamage = 5;
    [SerializeField] private bool canMove;
    [SerializeField] private float destroyThreshold = 0.1f;
    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    private void Update()
    {
        Vector3 direction = target - transform.position;
        transform.Translate(direction.normalized * projectileSpeed * Time.deltaTime, Space.World);

        // Destroy if close to target position
        if (Vector3.Distance(transform.position, target) <= destroyThreshold)
        {
            Destroy(gameObject);
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

    }
}
