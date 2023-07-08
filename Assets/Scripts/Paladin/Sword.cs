using UnityEngine;

public class Sword : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private float closeDistanceThreshold = 0.5f; // Threshold to consider the sword close to the target
    [SerializeField] private float swordSpeed = 5f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canKill = true;
    [HideInInspector] public bool moveTowardsMage = false;
    public void SetTarget(Vector3 target, float swordspeed)
    {
        this.target = target;
        swordSpeed = swordspeed;
    }

    private void Update()
    {
        if (canMove)
        {
            Vector3 direction = target - transform.position;
            transform.Translate(direction.normalized * swordSpeed * Time.deltaTime, Space.World);

            // Check if the sword is close to the target
            float distanceToTarget = Vector3.Distance(transform.position, target);
            if (distanceToTarget <= closeDistanceThreshold)
            {
                canMove = false;
                canKill = false;
            }
        }
        if (moveTowardsMage)
        {
            canMove = false;
            Vector3 wantedTarget = FindObjectOfType<MageBehaviour>().gameObject.transform.position;
            canKill = true;
            canMove = false;

            Vector3 direction = wantedTarget - transform.position;
            transform.Translate(direction.normalized * swordSpeed * Time.deltaTime, Space.World);

            // Check if the sword is close to the target
            float distanceToTarget = Vector3.Distance(transform.position, target);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && canKill)
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                if (monster.canTakeDamage)
                {
                    canKill = false;
                    canMove = false;
                    monster.Kill();
                }
            }
        }
        if (other.CompareTag("Mage"))
        {
            canMove = false;
            canKill = false;
            MageMovement mageMovement = other.GetComponent<MageMovement>();
            transform.SetParent(mageMovement.transform);
        }
        if (other.CompareTag("Paladin"))
        {
            PaladinSwordReset paladinSwordReset = other.gameObject.GetComponentInChildren<PaladinSwordReset>();
            paladinSwordReset.paladinBehaviour.canThrow = true;
            Destroy(gameObject);
        }
    }
}
