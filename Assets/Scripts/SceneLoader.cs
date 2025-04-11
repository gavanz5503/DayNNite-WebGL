using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        Time.timeScale = 1f; // Unpause the game when transitioning
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // For future use
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void LoadLevel1()
    {
        Time.timeScale = 1f; // Ensure game isn't paused from previous level
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed"); // Shows in editor
        Application.Quit(); // Quits in standalone build
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
