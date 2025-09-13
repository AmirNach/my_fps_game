using UnityEngine;
using TMPro;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    public Transform MuzzlePoint; // Point from which bullets are fired
    public GameObject BulletPrefab; // Bullet prefab to spawn bullets
    public TMP_Text AmmoText; // Reference to the ammo UI text
    public GameObject WeaponPivot; // Pivot for the reload animation

    [Header("Ammo Settings")]
    public int MagazineSize = 30; // Max bullets in magazine
    public int CurrentAmmo; // Current bullets in magazine
    public int ReserveAmmo = 90; // Bullets in reserve
    public float ReloadTime = 1f; // Time to reload

    [Header("Bullet Settings")]
    public float BulletSpeed = 70f; // Speed of bullets
    public float BulletLifetime = 3f; // Time before the bullet is destroyed

    private bool IsReloading = false; // Flag for reload state

    void Start()
    {
        CurrentAmmo = MagazineSize; // Initialize ammo
        UpdateAmmoUI(); // Update UI on start
    }

    void Update()
    {
        // Fire input
        if (Input.GetButtonDown("Fire") && !IsReloading)
        {
            Shoot();
        }

        // Reload input
        if (Input.GetButtonDown("Reload") && !IsReloading)
        {
            StartCoroutine(Reload());
        }
    }

    public void UpdateAmmoUI()
    {
        AmmoText.text = "Ammo: " + CurrentAmmo + "/" + ReserveAmmo; // Update UI text
    }

    void Shoot()
    {
        if (CurrentAmmo <= 0)
        {
            return;
        }

        GameObject Bullet = Instantiate(BulletPrefab, MuzzlePoint.position, MuzzlePoint.rotation); // Spawn bullet

        Rigidbody RB = Bullet.GetComponent<Rigidbody>();
        if (RB != null)
        {
            RB.velocity = MuzzlePoint.forward * BulletSpeed; // Set bullet velocity
        }

        Destroy(Bullet, BulletLifetime); // Destroy bullet after lifetime

        CurrentAmmo--; // Reduce ammo count
        UpdateAmmoUI(); // Update UI
    }

    IEnumerator Reload()
    {
        if (ReserveAmmo <= 0 || CurrentAmmo == MagazineSize)
        {
            yield break; // Exit if no need to reload
        }

        IsReloading = true; // Set reloading flag
        StartCoroutine(ReloadAnimation(ReloadTime)); // Start reload animation

        yield return new WaitForSeconds(ReloadTime); // Wait for reload duration

        int Needed = MagazineSize - CurrentAmmo;
        int AmmoToLoad = Mathf.Min(Needed, ReserveAmmo); // Calculate ammo to load

        CurrentAmmo += AmmoToLoad; // Add to magazine
        ReserveAmmo -= AmmoToLoad; // Reduce reserve ammo

        IsReloading = false; // Clear reloading flag

        UpdateAmmoUI(); // Update UI
    }

    IEnumerator ReloadAnimation(float Duration)
    {
        float Elapsed = 0f;
        while (Elapsed < Duration)
        {
            float RotationThisFrame = (360f / Duration) * Time.deltaTime;
            WeaponPivot.transform.Rotate(RotationThisFrame, 0f, 0f, Space.Self); // Rotate pivot
            Elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
