using UnityEngine;

public class Sword : MonoBehaviour
{
    private Vector3 target;
    public Sprite floatingSword;
    [SerializeField] private float closeDistanceThreshold = 0.5f; // Threshold to consider the sword close to the target
    [HideInInspector] public float swordSpeed = 5f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canKill = true;
    [HideInInspector] public bool moveTowardsMage = false;
    [HideInInspector] public bool isGrabbed = false;
    [HideInInspector] public bool rotateAroundMage = false;

    private float currentAngle; // Current angle of the object
    public float radius = 5f; // Radius of the circular motion
    public float rotationSpeed = 2f; // Speed of the rotation

    private MageMovement mageMovement;

    public void SetTarget(Vector3 target, float swordspeed)
    {
        this.target = target;
        swordSpeed = swordspeed;
    }

    private void Awake()
    {
        mageMovement = FindObjectOfType<MageMovement>();
    }

    private void Start()
    {
        canKill = true;
        canMove = true;
        rotateAroundMage = false;
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

        if (rotateAroundMage)
        {
            Vector2 centerPoint; // Center point of the circular motion
            bool isClockwise = false;

            centerPoint = mageMovement.transform.position;
            // Calculate the target angle based on the current time and speed
            float targetAngle = currentAngle + (isClockwise ? -1f : 1f) * rotationSpeed * Time.deltaTime;

            // Calculate the target position based on the angle and radius
            Vector2 targetPosition = centerPoint + new Vector2(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle)) * radius;

            // Set the Y rotation based on the position in relation to the radius
            if (targetPosition.x > centerPoint.x)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Right side of the radius
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Left side of the radius
            }

            // Move the object to the target position
            transform.position = targetPosition;

            // Update the current angle
            currentAngle = targetAngle;
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
            isGrabbed = true;
            rotateAroundMage = true;
            MageMovement mageMovement = other.GetComponent<MageMovement>();
            transform.SetParent(mageMovement.transform);

            SpriteRenderer spriterend = GetComponent<SpriteRenderer>();
            spriterend.sprite = floatingSword;
        }


        if (other.CompareTag("SwordWall"))
        {
            Debug.Log("collided w wall");

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
