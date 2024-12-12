using UnityEngine;

public class Car_Manager : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float turnSpeed = 50f;
    public float drag = 2f;
    public float wheelRadius = 1f;
    public float brakeStrength = 5f;

    // Steering parents
    public Transform frontLeftSteering;
    public Transform frontRightSteering;

    // Rolling tires
    public Transform Wheel_FL;
    public Transform Wheel_RL;
    public Transform Wheel_RR;
    public Transform Wheel_FR;

    private Rigidbody rb;
    private float currentSpeed = 0f;
    private float inputVertical = 0f;
    private float inputHorizontal = 0f;
    private bool isBraking = false;
    private float inputGasPedal = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 1500f; // Typical car mass in kg
        rb.drag = 0.1f;  // Low drag for a natural slowdown
        rb.angularDrag = 2f; // Higher angular drag to reduce spinning/rolling
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    private void FixedUpdate()
    {
        // Get input
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");
        //inputGasPedal = Input.GetAxis("RZ");
        Debug.Log(inputGasPedal);



        // Braking logic
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, brakeStrength * Time.deltaTime);
        }
        else
        {
            isBraking = false;
        }

        if (!isBraking)
        {
            // Accelerate or decelerate
            if (inputVertical > 0)
            {
                currentSpeed += inputVertical * acceleration * Time.deltaTime;
            }
            else
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
            }

            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

            // Movement
            Vector3 moveDirection = transform.forward * currentSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveDirection);

            // Turning
            if (currentSpeed != 0)
            {
                float turn = inputHorizontal * turnSpeed * Time.deltaTime * Mathf.Sign(currentSpeed);
                rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
            }
        }

        // Rotate and steer wheels
        RotateWheels();
        SteerWheels();
    }

    private void ApplyBrake()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeStrength * Time.deltaTime / rb.mass);

        // Gradually slow down angular velocity for stability
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, brakeStrength * Time.deltaTime / rb.mass);

        // Optionally, log or display braking for debugging
        Debug.Log("Slowing down smoothly!");
    }

    private void StabilizeCar()
    {
        RaycastHit hit;
        float suspensionStrength = 500f;
        float suspensionDamping = 10f;
        float suspensionHeight = 0.5f;

        if (Physics.Raycast(transform.position, -transform.up, out hit, suspensionHeight + 0.1f))
        {
            float compressionRatio = (suspensionHeight - hit.distance) / suspensionHeight;
            if (compressionRatio > 0)
            {
                rb.AddForceAtPosition(transform.up * compressionRatio * suspensionStrength, hit.point);
                rb.AddForceAtPosition(-rb.velocity * suspensionDamping, hit.point);
            }
        }
    }

    private void RotateWheels()
    {
        float rotationAmount = currentSpeed * 360f / (2f * Mathf.PI * wheelRadius) * Time.deltaTime;
        Vector3 wheelRotation = new Vector3(rotationAmount, 0f, 0f);

        Wheel_FL.Rotate(wheelRotation, Space.Self);
        Wheel_RL.Rotate(wheelRotation, Space.Self);
        Wheel_RR.Rotate(wheelRotation, Space.Self);
        Wheel_FR.Rotate(wheelRotation, Space.Self);
    }

    private void SteerWheels()
    {
        float steeringAngle = inputHorizontal * 30f;
        frontLeftSteering.localRotation = Quaternion.Euler(0f, steeringAngle, 0f);
        frontRightSteering.localRotation = Quaternion.Euler(0f, steeringAngle, 0f);
    }
}