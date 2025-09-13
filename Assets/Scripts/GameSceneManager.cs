using UnityEngine;
using UnityEngine.SceneManagement;

// Handles scene loading using singleton pattern
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance; // Singleton instance

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Stay existed between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu"); // Load main menu scene
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene"); // Load game scene
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene"); // Load win scene
        Invoke("LoadMainMenu", 3f); // Return to the main menu after 3 seconds
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScene"); // Load lose scene
        Invoke("LoadMainMenu", 3f); // Return to the main menu after 3 seconds
    }

    public void ExitGame()
    {
        Application.Quit(); // Quit the game
    }
}
