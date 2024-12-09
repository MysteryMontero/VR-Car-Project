using System.Collections;
using UnityEngine;

public class SingleTrafficLightController : MonoBehaviour
{
    public Renderer lightRenderer; // Renderer to visually represent the light (assign in Inspector)
    public Color greenColor = Color.green;
    public Color yellowColor = Color.yellow;
    public Color redColor = Color.red;

    public float greenTime = 10f;   // Duration for green light
    public float yellowTime = 3f;  // Duration for yellow light
    public float redTime = 10f;    // Duration for red light

    private void Start()
    {
        // Start cycling through the light states
        StartCoroutine(TrafficLightCycle());
    }

    private IEnumerator TrafficLightCycle()
    {
        while (true)
        {
            // Green Light
            SetLightState("Green");
            yield return new WaitForSeconds(greenTime);

            // Yellow Light
            SetLightState("Yellow");
            yield return new WaitForSeconds(yellowTime);

            // Red Light
            SetLightState("Red");
            yield return new WaitForSeconds(redTime);
        }
    }

    private void SetLightState(string state)
    {
        if (lightRenderer == null)
        {
            Debug.LogError("Light Renderer not assigned!");
            return;
        }

        // Change the material color of the light based on the state
        switch (state)
        {
            case "Green":
                lightRenderer.material.color = greenColor;
                break;
            case "Yellow":
                lightRenderer.material.color = yellowColor;
                break;
            case "Red":
                lightRenderer.material.color = redColor;
                break;
            default:
                Debug.LogWarning("Unknown state: " + state);
                break;
        }
    }
}