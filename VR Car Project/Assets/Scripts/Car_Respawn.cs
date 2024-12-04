using UnityEngine;

public class Car_Respawn : MonoBehaviour
{
    private Vector3 respawnPosition; // To store the initial position
    private Quaternion respawnRotation; // To store the initial rotation

    public string groundTag = "Ground"; // Tag for ground objects
    public string checkpointTag = "Checkpoint"; // Tag for checkpoint objects

    private void Start()
    {
        // Save the starting position and rotation
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the car touches an object tagged as "Ground"
        if (collision.collider.CompareTag(groundTag))
        {
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the car passes a checkpoint
        if (other.CompareTag(checkpointTag))
        {
            // Update the respawn position and rotation to the checkpoint
            respawnPosition = other.transform.position;
            respawnRotation = other.transform.rotation;

            Debug.Log("Checkpoint reached!");

            Destroy(other.gameObject);
        }
    }

    private void Respawn()
    {
        // Reset position and rotation
        transform.position = respawnPosition;
        transform.rotation = respawnRotation;

        // Reset velocity (if using Rigidbody)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}