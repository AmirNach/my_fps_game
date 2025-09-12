using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int MaxHealth = 100;
    public int Damage = 20;
    public float AttackCooldown = 5f;
    public float Speed = 10f;

    [Header("UI")]
    public TMP_Text HealthText;

    private int CurrentHealth;
    private float LastAttackTime = -999f;
    private Transform Player;
    private NavMeshAgent Agent;

    void Start()
    {
        CurrentHealth = MaxHealth;

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = Speed;

        UpdateHealthText();
    }

    void Update()
    {
        if (Player != null)
        {
            Agent.SetDestination(Player.position);
        }

        if (HealthText != null && Camera.main != null)
        {
            Vector3 lookDirection = Camera.main.transform.position - HealthText.transform.position;
            HealthText.transform.rotation = Quaternion.LookRotation(-lookDirection); // זווית נכונה מול המצלמה
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryAttack(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.Damage);
                Destroy(collision.gameObject);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryAttack(collision.gameObject);
        }
    }

    void TryAttack(GameObject playerObject)
    {
        if (Time.time > LastAttackTime + AttackCooldown)
        {
            PlayerHealth ph = playerObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(Damage);
            }
            LastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        UpdateHealthText();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthText()
    {
        if (HealthText != null)
        {
            HealthText.text = CurrentHealth.ToString();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
