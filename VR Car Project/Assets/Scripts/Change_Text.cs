using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Change_Text : MonoBehaviour
{
    public TextMeshProUGUI victoryText;
    public float delayTime = 3f;
    public string newMessage = "Get ready for the next challenge!";
    public float sceneChangeDelay = 6f; // Ensure this gives enough time for text change
    public string nextSceneName = "Road2";

    private void Start()
    {
        if (victoryText != null)
        {
            StartCoroutine(ChangeText());
        }

        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(delayTime);
        if (victoryText != null)
        {
            Debug.Log("Changing text to: " + newMessage);
            victoryText.text = newMessage;
        }
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(sceneChangeDelay);
        Debug.Log("Attempting to load scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}