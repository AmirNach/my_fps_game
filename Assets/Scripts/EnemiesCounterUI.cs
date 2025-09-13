using UnityEngine;
using TMPro;

// UI script to show the enemies count
public class EnemiesCounterUI : MonoBehaviour
{
    public TMP_Text EnemiesText; 

    private int enemiesAlive; // Number of alive enemies

    void Start()
    {
        // Look for all the enemies alvie
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateText();
    }

    public void EnemyDied()
    {
        enemiesAlive--; // Decrease the count when enemy dies
        UpdateText();

        // Check for win condition
        if (enemiesAlive <= 0)
        {
            GameSceneManager.Instance.LoadWinScene();
        }
    }

    // Update the UI alive enemies text
    private void UpdateText()
    {
        EnemiesText.text = "Enemies Alive: " + enemiesAlive; 
    }
}
