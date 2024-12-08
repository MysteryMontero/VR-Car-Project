using System.Collections;
using UnityEngine;

public class AutoCloseGame : MonoBehaviour
{
    public float delayBeforeExit = 3f; // Time to wait before closing the game

    private void Start()
    {
        StartCoroutine(CloseGameAfterDelay());
    }

    private IEnumerator CloseGameAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeExit);

        // Quit the application
        Application.Quit();

        // This will only work in a built application. For testing in the editor:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}