using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deermovement : MonoBehaviour
{
    public float moveSpeed = 3f;      // Speed of the animal
    public float moveDistance = 5f;  // Distance the animal moves forward and backward
    public float pauseDuration = 1f; // Pause duration at each end
    private float timer = 0f;        // Timer to manage pauses

    private Vector3 startPoint;      // Starting position of the animal
    private Vector3 endPoint;        // End position of the movement
    private bool movingForward = true; // Direction of movement
    private Rigidbody rb;            // Reference to the Rigidbody

    private void Start()
    {
        // Set the starting position of the animal
        startPoint = transform.position;
        endPoint = startPoint + transform.forward * moveDistance;

        // Get the Rigidbody and constrain unwanted rotations
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationZ |
                             RigidbodyConstraints.FreezePositionY; 
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            // Pause the movement
            timer -= Time.deltaTime;
            return;
        }

        // Move the animal forward or backward
        if (movingForward)
        {
            MoveAnimal(endPoint);

            // Check if the animal reached the end point
            if (Vector3.Distance(transform.position, endPoint) < 0.2f)
            {
                movingForward = false;
                timer = pauseDuration;
                TurnAround();
            }
        }
        else
        {
            MoveAnimal(startPoint);

            // Check if the animal reached the starting point
            if (Vector3.Distance(transform.position, startPoint) < 0.2f)
            {
                movingForward = true;
                timer = pauseDuration;
                TurnAround();
            }
        }
    }

    private void MoveAnimal(Vector3 targetPosition)
    {
        // Use Rigidbody for smooth physics-based movement
        if (rb != null)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void TurnAround()
    {
        // Rotate the animal by 180 degrees around the Y-axis
        transform.Rotate(0f, 180f, 0f);
    }
}
