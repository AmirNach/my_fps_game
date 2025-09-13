using UnityEngine;

// Bullet Script
public class Bullet : MonoBehaviour
{
    public int Damage = 20; // Damage dealt by this bullet

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Destroy the bullet on impact
    }
}
