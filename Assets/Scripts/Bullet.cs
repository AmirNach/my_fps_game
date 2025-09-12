using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 20;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
