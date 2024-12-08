using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleCarMovement : MonoBehaviour
{
    public float waitTime = 3f; // Time to wait before moving
    public float moveSpeed = 100f; // Speed of the car
    public Transform target; // Player or direction to move towards
    public string nextSceneName = "GameOver"; // Name of the next scene

    private bool isMoving = false; // To check if the car should start moving

    private void Start()
    {
        // Start a coroutine to delay movement
        StartCoroutine(WaitAndMove());
    }

    private IEnumerator WaitAndMove()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Start moving
        isMoving = true;
    }

    private void Update()
    {
        // If the car is set to move
        if (isMoving && target != null)
        {
            // Move towards the target's position
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the obstacle car hits the player
        if (collision.collider.CompareTag("Player"))
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}