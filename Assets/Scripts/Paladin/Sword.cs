using UnityEngine;

public class Sword : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private float closeDistanceThreshold = 0.5f; // Threshold to consider the sword close to the target
    [HideInInspector] public float swordSpeed = 5f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canKill = true;
    [HideInInspector] public bool moveTowardsMage = false;
    [HideInInspector] public bool isGrabbed = false;
    public void SetTarget(Vector3 target, float swordspeed)
    {
        this.target = target;
        swordSpeed = swordspeed;
    }

    private void Start()
    {
        canKill = true;
        canMove = true;
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
                    GameManager.Instance.ChangeScore();
                }
            }
        }
        if (other.CompareTag("Mage"))
        {
            canMove = false;
            canKill = false;
            isGrabbed = true;
            MageMovement mageMovement = other.GetComponent<MageMovement>();
            transform.SetParent(mageMovement.transform);
        }


        if (other.CompareTag("SwordWall"))
        {
            if (canKill)
            {
                Debug.Log("collided w wall");
                PaladinBehaviour paladinBehaviour = FindObjectOfType<PaladinBehaviour>();
                paladinBehaviour.DestroyedByWall();
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        // PaladinBehaviour paladinBehaviour = FindObjectOfType<PaladinBehaviour>();
        // paladinBehaviour.DestroyedByWall();
    }

    public void DestroySword()
    {
        Destroy(gameObject);
    }

}
