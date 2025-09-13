using UnityEngine;
using UnityEngine.AI;
using TMPro;

// Enemy movement and health script
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int MaxHealth = 100;
    public int Damage = 20;
    public float AttackCooldown = 5f;
    public float Speed = 10f;

    [Header("UI")]
    public TMP_Text HealthText;
    private EnemiesCounterUI counterUI; 

    private int CurrentHealth;
    private float LastAttackTime = -999f;
    private Transform PlayerTransform;
    private NavMeshAgent Agent;

    void Start()
    {
        CurrentHealth = MaxHealth;

        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = Speed;

        counterUI = FindObjectOfType<EnemiesCounterUI>();
        if (counterUI == null)
            Debug.LogWarning("EnemiesCounterUI not found in the scene");

        UpdateHealthText();
    }

    void Update()
    {
        if (PlayerTransform != null)
            Agent.SetDestination(PlayerTransform.position); // Follow the player

        if (HealthText != null && Camera.main != null)
        {
            // Make the enemy's health UI face the camera
            Vector3 lookDirection = Camera.main.transform.position - HealthText.transform.position;
            HealthText.transform.rotation = Quaternion.LookRotation(-lookDirection);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            TryAttack(collision.gameObject);

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.Damage); // Enemy Takes damage from the bullet
                Destroy(collision.gameObject);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            TryAttack(collision.gameObject);
    }

    // Try to attack the player
    // The attack has cooldown so the attack may not be always available
    void TryAttack(GameObject playerObject)
    {
        if (Time.time > LastAttackTime + AttackCooldown)
        {
            PlayerHealth ph = playerObject.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(Damage); // Damage the player

            LastAttackTime = Time.time;
        }
    }

    // Enemy takes damage
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        UpdateHealthText();

        if (CurrentHealth <= 0)
            Die();
    }

    // Update UI health text
    void UpdateHealthText()
    {
        if (HealthText != null)
            HealthText.text = CurrentHealth.ToString();
    }

    // Kill the enemy
    void Die()
    {
        if (counterUI != null)
            counterUI.EnemyDied(); // Notify UI

        Destroy(gameObject);
    }
}
