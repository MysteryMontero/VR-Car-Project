using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string victorySceneName = "Finish"; // Replace with your victory scene name

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the player's car
        if (other.CompareTag("Player")) // Make sure your car is tagged as "Player"
        {
            LoadVictoryScene();
        }
    }

    private void LoadVictoryScene()
    {
        SceneManager.LoadScene(victorySceneName);
    }
}