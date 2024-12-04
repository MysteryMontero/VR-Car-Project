using UnityEngine;

public class Rotation_Car : MonoBehaviour
{
    public Transform centerPoint; // The center of the circular path
    public float radius = 10f;    // Radius of the circle
    public float speed = 5f;      // Speed of movement

    private float angle = 0f;     // Current angle on the circular path

    private void Update()
    {
        // Update the angle based on speed and time
        angle += speed * Time.deltaTime;

        // Keep the angle within 0 to 360 degrees
        if (angle > 360f)
            angle -= 360f;

        // Calculate the new position using trigonometry
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float z = centerPoint.position.z + Mathf.Sin(angle) * radius;

        // Update the position of the object
        transform.position = new Vector3(x, transform.position.y, z);

        // Optional: Face the direction of movement
        Vector3 direction = new Vector3(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
        transform.forward = direction;
    }
}