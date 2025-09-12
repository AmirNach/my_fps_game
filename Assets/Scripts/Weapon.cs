using UnityEngine;
using TMPro;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    public Transform MuzzlePoint;
    public GameObject BulletPrefab;
    public TMP_Text AmmoText;
    public GameObject WeaponPivot;

    [Header("Ammo Settings")]
    public int MagazineSize = 30;
    public int CurrentAmmo;
    public int ReserveAmmo = 90;
    public float ReloadTime = 1f;

    [Header("Bullet Settings")]
    public float BulletSpeed = 100f;
    public float BulletLifetime = 3f;

    private bool IsReloading = false;

    void Start()
    {
        CurrentAmmo = MagazineSize;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire") && !IsReloading)
        {
            Shoot();
        }

        if (Input.GetButtonDown("Reload") && !IsReloading)
        {
            StartCoroutine(Reload());
        }
    }

    public void UpdateAmmoUI()
    {
        AmmoText.text = "Ammo: " + CurrentAmmo + " / " + ReserveAmmo;
    }

    void Shoot()
    {
        if (CurrentAmmo <= 0)
        {
            Debug.Log("No ammo left in the magazine");
            return;
        }

        GameObject Bullet = Instantiate(BulletPrefab, MuzzlePoint.position, MuzzlePoint.rotation);

        Rigidbody RB = Bullet.GetComponent<Rigidbody>();
        if (RB != null)
        {
            RB.velocity = MuzzlePoint.forward * BulletSpeed;
        }

        Destroy(Bullet, BulletLifetime);

        CurrentAmmo--;
        UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        if (ReserveAmmo <= 0 || CurrentAmmo == MagazineSize)
        {
            yield break;
        }

        IsReloading = true;
        StartCoroutine(ReloadAnimation(ReloadTime));

        yield return new WaitForSeconds(ReloadTime);

        int Needed = MagazineSize - CurrentAmmo;
        int AmmoToLoad = Mathf.Min(Needed, ReserveAmmo);

        CurrentAmmo += AmmoToLoad;
        ReserveAmmo -= AmmoToLoad;

        IsReloading = false;

        UpdateAmmoUI();
    }
    IEnumerator ReloadAnimation(float Duration)
    {
        float Elapsed = 0f;
        while (Elapsed < Duration)
        {
            float RotationThisFrame = (360f / Duration) * Time.deltaTime; 
            WeaponPivot.transform.Rotate(RotationThisFrame, 0f, 0f, Space.Self);
            Elapsed += Time.deltaTime;
            yield return null;
        }
    }

}
