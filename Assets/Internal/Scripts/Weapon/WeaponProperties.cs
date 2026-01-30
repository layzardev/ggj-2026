using UnityEngine;

public class WeaponProperties : MonoBehaviour
{
    [SerializeField] int _weaponDamage = 1;
    [SerializeField] int _maxBullet = 12;
    [SerializeField] int _currentBulletAmount;
    [SerializeField] float _reloadTime = 1.5f;
    [SerializeField] float _shootRate = 0.5f;
    [SerializeField] public GameObject _bulletPrefab;
    public int WeaponDamage => _weaponDamage;
    public virtual void ShootWeapon()
    {
        Debug.Log("Shooting Projectile Weapon");
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<PlayerProjectile>().InitializeProjectile(this,this.transform.forward);
    }

}
