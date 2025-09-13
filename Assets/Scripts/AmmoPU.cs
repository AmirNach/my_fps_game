using UnityEngine;

// Ammo PowerUp Script
public class AmmoPU : MonoBehaviour
{
    private const int AmmoAmountToAdd = 30; // Amount to add on pickup
    public GameObject weaponHolder; // Reference to player's weapon

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (weaponHolder != null)
            {
                // Add ammo to player's weapon
                weaponHolder.GetComponent<Weapon>().ReserveAmmo += AmmoAmountToAdd;
                weaponHolder.GetComponent<Weapon>().UpdateAmmoUI(); // Refresh UI
            }

            Destroy(gameObject); // Destroy the powerup gameObject
        }
    }
}
