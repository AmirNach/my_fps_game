using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Player health script
public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    public int MaxHealth = 100; // Max health value
    public int CurrentHealth;   // Current health value
    public TMP_Text HealthText; // Reference to the UI text

    void Start()
    {
        CurrentHealth = MaxHealth; // Initialize player health
        UpdateHealthUI();          // Update UI on start
    }

    // Called when the player takes damage
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage; // Reduce the player's health
        Debug.Log("Player took damage! Health: " + CurrentHealth);
        UpdateHealthUI();        // Refresh UI

        if (CurrentHealth <= 0)
        {
            Die(); // Death
        }
    }

    // Called to heal the player
    public void Heal(int amount)
    {
        CurrentHealth += amount; // Add health
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth; // Set health to max
        }

        Debug.Log("Player healed! Health: " + CurrentHealth);
        UpdateHealthUI(); // Refresh UI
    }

    // Update the health display on UI
    private void UpdateHealthUI()
    {
        if (HealthText != null)
        {
            HealthText.text = "Health: " + CurrentHealth + "/" + MaxHealth;
        }
    }

    // Called when the player dies
    private void Die()
    {
        Debug.Log("Player died!");
        GameSceneManager.Instance.LoadLoseScene(); // Load lose scene
    }

    // Return to the main menu
    private void ReturnToMainMenu()
    {
        GameSceneManager.Instance.LoadMainMenu(); // Load main menu scene
    }
}
