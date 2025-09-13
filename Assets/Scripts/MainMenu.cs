using UnityEngine;
using UnityEngine.SceneManagement;

// Handles main menu buttons
public class MainMenu : MonoBehaviour
{
    // Called when pressing the "Start" button
    public void StartGame()
    {
        GameSceneManager.Instance.LoadGame(); // Load the game scene
    }

    // Called when pressing the "Exit" button
    public void ExitGame()
    {
        GameSceneManager.Instance.ExitGame(); // Quit the game
    }
}
