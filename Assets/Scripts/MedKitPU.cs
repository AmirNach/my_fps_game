using UnityEngine;

// MedKit Powerup script
public class MedKitPU : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); // Get access to the player's health script

        if (playerHealth != null)
        {
            int healAmount = playerHealth.MaxHealth / 2; // Calculate half of max health

            if (playerHealth.CurrentHealth < playerHealth.MaxHealth)
            {
                playerHealth.Heal(healAmount); // Heal the player
            }

            Destroy(gameObject); // Destroy the medkit
        }
    }
}
