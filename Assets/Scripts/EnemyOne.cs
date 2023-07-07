using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    public EnemyStats enemyStats;

    private Transform mageTransform;

    private void Start()
    {
        mageTransform = FindObjectOfType<MageBehaviour>().transform;
    }

    private void Update()
    {
        Vector2 direction = mageTransform.position - transform.position;
        transform.Translate(direction.normalized * enemyStats.speed * Time.deltaTime);
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
