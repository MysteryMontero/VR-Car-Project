using UnityEngine;

public class Car_Manager : MonoBehaviour
{
    public float maxSpeed = 20f;       // Maximum forward speed
    public float acceleration = 5f;   // How quickly the car accelerates
    public float deceleration = 10f;  // How quickly the car slows down when not accelerating
    public float brakeForce = 20f;    // How strong the braking is
    public float turnSpeed = 50f;     // Turning speed
    public float drag = 2f;           // Natural slowing effect when no input is applied
    public Transform steer;

    private Rigidbody rb;
    private float currentSpeed = 0f;  // Current speed of the car
    private float inputVertical = 0f; // Player's vertical input
    private float inputHorizontal = 0f; // Player's horizontal input
    private float maxSteeringAngle = 30f; // Max steering angle for the wheel

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag; // Set drag for realistic slowing
    }

    private void FixedUpdate()
    {
        // Get input from the player
        inputVertical = Input.GetAxis("Vertical");   // W/S or Up/Down keys
        inputHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right keys

        // Check for braking input
        if (Input.GetKey(KeyCode.Q)) // Brake when "Q" is pressed
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, brakeForce * Time.deltaTime);
        }
        else
        {
            // Accelerate or decelerate based on input
            if (inputVertical != 0)
            {
                currentSpeed += inputVertical * acceleration * Time.deltaTime;
            }
            else
            {
                // Gradually slow down when no input is given
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
            }
        }

        // Clamp the speed to the maximum allowed
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // Apply movement
        Vector3 moveDirection = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Turning
        if (currentSpeed != 0) // Only turn when the car is moving
        {
            float turn = inputHorizontal * turnSpeed * Time.deltaTime * Mathf.Sign(currentSpeed);

            // Reduce turning at higher speeds but not completely
            float speedFactor = Mathf.Abs(currentSpeed) / maxSpeed;
            turn *= Mathf.Clamp01(1 - speedFactor * 0.3f); // Decrease turning but keep a minimum value

            // Apply the rotation
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
            if (steer != null)
            {
                // Rotate the steering wheel based on input
                float steeringRotation = inputHorizontal * maxSteeringAngle;
                steer.localRotation = Quaternion.Euler(0f, 0f, steeringRotation);
            }
        }
    }
}
