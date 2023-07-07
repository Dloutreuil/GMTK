using UnityEngine;

public class PaladinMovement : MonoBehaviour
{
    public float radius = 5f; // Radius of the circular motion
    public float rotationSpeed = 2f; // Speed of the rotation

    private Vector2 centerPoint; // Center point of the circular motion
    private bool isMoving = false; // Flag to indicate if the object is moving
    private bool isClockwise = false; // Flag to indicate the movement direction

    private float currentAngle; // Current angle of the object

    private void Start()
    {
        centerPoint = transform.position; // Set the initial center point to the object's position
    }

    private void Update()
    {
        // Check for left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true; // Start moving
            isClockwise = false; // Move counterclockwise
        }
        // Check for right mouse click
        else if (Input.GetMouseButtonDown(1))
        {
            isMoving = true; // Start moving
            isClockwise = true; // Move clockwise
        }
        // Check if there are no mouse clicks
        else if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            isMoving = false; // Stop moving
        }

        // Move only if the object is flagged to move
        if (isMoving)
        {
            // Calculate the target angle based on the current time and speed
            float targetAngle = currentAngle + (isClockwise ? -1f : 1f) * rotationSpeed * Time.deltaTime;

            // Calculate the target position based on the angle and radius
            Vector2 targetPosition = centerPoint + new Vector2(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle)) * radius;

            // Move the object to the target position
            transform.position = targetPosition;

            // Update the current angle
            currentAngle = targetAngle;
        }
    }
}
