using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public string gameSceneName = "Road"; // Replace with your actual scene name

    void Update()
    {
        // Check for spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadGameScene();
        }
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName); // Load the specified game scene
    }
}