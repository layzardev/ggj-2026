using UnityEngine;

public class ProjectileWeapon : WeaponProperties
{
    [SerializeField] GameObject _bulletPrefab;
    override public void ShootWeapon()
    {
        if (_currentBulletAmount <= 0)
        {
            Debug.Log("Out of Ammo, Reload!");
            StartCoroutine(ReloadWeapon());
            return;
        }
        _currentBulletAmount--;
        Debug.Log("Shooting Projectile Weapon");
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<PlayerProjectile>().InitializeProjectile(this, this.transform.forward);
    }
}
