using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;
    public TMP_Text HealthText;


    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthText.text = "Health: " + CurrentHealth + "/" + MaxHealth;
    }

    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        Debug.Log("Player took damage! Health: " + CurrentHealth);

        HealthText.text = "Health: " + CurrentHealth + "/" + MaxHealth;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        Destroy(gameObject);
    }
}
