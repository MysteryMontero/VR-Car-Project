using UnityEngine;

public class Car_Manager : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float turnSpeed = 50f;
    public float drag = 2f;

    // Steering parents
    public Transform frontLeftSteering;
    public Transform frontRightSteering;

    // Rolling tires
    public Transform frontLeftTire;
    public Transform frontRightTire;
    public Transform rearLeftTire;
    public Transform rearRightTire;

    private Rigidbody rb;
    private float currentSpeed = 0f;
    private float inputVertical = 0f;
    private float inputHorizontal = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
    }

    private void FixedUpdate()
    {
        // Get input
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");

        // Accelerate or decelerate
        if (inputVertical != 0)
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

        // Rotate and steer wheels
        RotateWheels();
        SteerWheels();
    }

    private void RotateWheels()
    {
        float rotationAmount = currentSpeed * 360f / (2f * Mathf.PI * 0.35f) * Time.deltaTime;
        Vector3 wheelRotation = new Vector3(rotationAmount, 0f, 0f);

        frontLeftTire.Rotate(wheelRotation, Space.Self);
        frontRightTire.Rotate(wheelRotation, Space.Self);
        rearLeftTire.Rotate(wheelRotation, Space.Self);
        rearRightTire.Rotate(wheelRotation, Space.Self);
    }

    private void SteerWheels()
    {
        float steeringAngle = inputHorizontal * 30f;
        frontLeftSteering.localRotation = Quaternion.Euler(0f, steeringAngle, 0f);
        frontRightSteering.localRotation = Quaternion.Euler(0f, steeringAngle, 0f);
    }
}