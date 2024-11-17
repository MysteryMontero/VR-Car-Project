using UnityEngine;

public class ChangePivot : MonoBehaviour
{
    void Start()
    {
        // Example: Move the object's position to simulate a pivot change to its center
        Vector3 centerOfObject = GetMeshCenter();
        transform.position = centerOfObject;
    }

    // Helper function to calculate the mesh's center
    Vector3 GetMeshCenter()
    {
        // Assuming the object has a MeshRenderer attached to it
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // Calculate the bounds' center of the object's mesh
            return meshRenderer.bounds.center;
        }
        else
        {
            return transform.position; // Default to the object's current position
        }
    }
    
    public Transform newPivot; // Drag the empty GameObject (pivot) here in the inspector

    void Update()
    {
        // Rotate the car/steering wheel using the new pivot
        transform.RotateAround(newPivot.position, Vector3.up, 10f * Time.deltaTime);
    }
}