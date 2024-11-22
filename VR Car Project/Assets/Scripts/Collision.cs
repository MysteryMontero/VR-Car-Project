using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collison : MonoBehaviour
{
    public float pushBackForce = 500f; // The force applied to the car on collision

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the deer is the car
        if (collision.gameObject.CompareTag("Car"))
        {
            // Get the Rigidbody of the car
            Rigidbody carRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (carRigidbody != null)
            {
                // Calculate the pushback direction (horizontal only)
                Vector3 pushDirection = collision.contacts[0].point - transform.position;
                pushDirection.y = 0; // Neutralize vertical movement
                pushDirection = -pushDirection.normalized; // Reverse and normalize the direction

                // Apply force to the car
                carRigidbody.AddForce(pushDirection * pushBackForce, ForceMode.Impulse);
            }
        }
    }
}
