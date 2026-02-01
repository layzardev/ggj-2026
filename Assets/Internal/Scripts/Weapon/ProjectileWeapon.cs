using UnityEngine;

public class ProjectileWeapon : WeaponProperties
{
    [SerializeField] GameObject _bulletPrefab;
     public override void ShootWeapon()
    {
        if (_delayTimer >= 0) return;

        if (_isReloading)
        {
            Debug.Log("Currently Reloading...");
            return;
        }
        if (_currentBulletAmount <= 0)
        {
            Debug.Log("Out of Ammo, Reload!");
            StartCoroutine(ReloadWeapon());
            return;
        }
        base.ShootWeapon();
        Debug.Log("Shooting Projectile Weapon");
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<PlayerProjectile>().InitializeProjectile(this, this.transform.forward);
    }
}
