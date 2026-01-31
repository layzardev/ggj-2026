using UnityEngine;

public class ProjectileWeapon : WeaponProperties
{
    [SerializeField] GameObject _bulletPrefab;
    override public void ShootWeapon()
    {
        base.ShootWeapon();
        Debug.Log("Shooting Projectile Weapon");
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<PlayerProjectile>().InitializeProjectile(this, this.transform.forward);
    }
}
