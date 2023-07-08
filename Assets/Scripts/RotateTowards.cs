using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    public Transform tip; // Reference to the tip of your sword
    public Vector3 targetPosition; // Target position to rotate towards

    private void Awake()
    {
        targetPosition = FindObjectOfType<MageBehaviour>().transform.position;
    }

    private void Start()
    {
        targetPosition = FindObjectOfType<MageBehaviour>().transform.position;

    }

    private void Update()
    {
        if (tip != null)
        {
            Vector3 direction = targetPosition - tip.position; // Calculate the direction towards the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate the angle in degrees
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Create a rotation quaternion

            transform.rotation = rotation; // Apply the rotation to the GameObject
        }
    }
}
