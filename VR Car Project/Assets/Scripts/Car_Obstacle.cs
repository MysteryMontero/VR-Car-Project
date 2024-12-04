using UnityEngine;

public class Car_Obstacle : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed of movement
    public float pauseDuration = 1f; // Pause duration at each turn

    public float[] moveDistances; // Array of move distances for each direction

    private Vector3[] directions; // Stores movement directions
    private int currentDirectionIndex = 0; // Index of the current direction
    private float timer = 0f; // Pause timer

    private Vector3 targetPosition; // Current target position
    private bool isPaused = false; // Whether movement is paused

    private void Start()
    {
        if (moveDistances.Length != 4)
        {
            Debug.LogError("You must define exactly 4 move distances, one for each direction.");
            return;
        }

        // Define movement directions (forward, right, backward, left)
        directions = new Vector3[]
        {
            Vector3.right,
            Vector3.forward,
            Vector3.left,
            Vector3.back
        };

        // Set the first target position
        SetNextTarget();
    }

    private void Update()
    {
        if (isPaused)
        {
            // Handle pause duration
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                isPaused = false;
                SetNextTarget();
            }
            return;
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the target position has been reached
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Pause before switching to the next direction
            isPaused = true;
            timer = pauseDuration;
        }
    }

    private void SetNextTarget()
    {
        // Calculate the next target position using the current direction and corresponding move distance
        float moveDistance = moveDistances[currentDirectionIndex];
        targetPosition = transform.position + directions[currentDirectionIndex] * moveDistance;

        // Rotate the object to face the new direction
        transform.rotation = Quaternion.LookRotation(directions[currentDirectionIndex]);

        // Move to the next direction in the sequence, looping back to the start
        currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;
    }
}