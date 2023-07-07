using System.Collections;
using UnityEngine;

public class PaladinMovement : MonoBehaviour
{
    public float radius = 5f; // Radius of the circular motion
    public float rotationSpeed = 2f; // Speed of the rotation

    private Vector2 centerPoint; // Center point of the circular motion
    private bool isMoving = false; // Flag to indicate if the object is moving
    private bool isClockwise = false; // Flag to indicate the movement direction

    private IEnumerator currentMovement; // Coroutine reference for the current movement
    private float initialAngle; // Initial angle when the movement starts

    private void Start()
    {
        centerPoint = transform.position; // Set the initial center point to the object's position
    }

    private void Update()
    {
        // Check for left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (!isMoving)
            {
                isMoving = true; // Start moving
                isClockwise = false; // Move counterclockwise

                if (currentMovement != null)
                {
                    StopCoroutine(currentMovement); // Stop the current movement coroutine if it exists
                }

                initialAngle = CalculateAngle(); // Store the initial angle

                currentMovement = MoveObject(); // Start a new movement coroutine
                StartCoroutine(currentMovement);
            }
        }
        // Check for right mouse click
        else if (Input.GetMouseButtonDown(1))
        {
            if (!isMoving)
            {
                isMoving = true; // Start moving
                isClockwise = true; // Move clockwise

                if (currentMovement != null)
                {
                    StopCoroutine(currentMovement); // Stop the current movement coroutine if it exists
                }

                initialAngle = CalculateAngle(); // Store the initial angle

                currentMovement = MoveObject(); // Start a new movement coroutine
                StartCoroutine(currentMovement);
            }
        }
        // Check if there are no mouse clicks
        else if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            isMoving = false; // Stop moving
        }
    }

    private IEnumerator MoveObject()
    {
        float currentAngle = initialAngle;

        while (isMoving)
        {
            // Calculate the target angle based on the current time and speed relative to the initial angle
            float targetAngle = currentAngle + (isClockwise ? -1f : 1f) * rotationSpeed * Time.deltaTime;

            // Calculate the target position based on the angle and radius
            Vector2 targetPosition = centerPoint + new Vector2(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle)) * radius;

            // Move the object to the target position
            transform.position = targetPosition;

            // Update the current angle
            currentAngle = targetAngle;

            yield return null;
        }
    }

    private float CalculateAngle()
    {
        Vector2 direction = (Vector2)transform.position - centerPoint;
        return Mathf.Atan2(direction.y, direction.x);
    }
}
