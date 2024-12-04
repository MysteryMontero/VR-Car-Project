using UnityEngine;

public class Checkpoint_Flag : MonoBehaviour
{
    private Vector3 respawnPosition; // To store the respawn position
    private Quaternion respawnRotation; // To store the respawn rotation

    public string checkpointTag = "Checkpoint"; // Tag for flag objects

    private void Start()
    {
        // Initialize respawn to the car's starting position and rotation
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the car touches a flag object tagged as "Checkpoint"
        if (other.CompareTag(checkpointTag))
        {
            // Update the respawn position and rotation to the checkpoint's position
            respawnPosition = other.transform.position;
            respawnRotation = other.transform.rotation;

            // Optional: Provide feedback for reaching a checkpoint
            Debug.Log("Checkpoint reached!");
        }
    }

    private void Respawn()
    {
        // Reset the car's position and rotation to the latest checkpoint
        transform.position = respawnPosition;
        transform.rotation = respawnRotation;

        // Reset velocity (if the car has a Rigidbody)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}