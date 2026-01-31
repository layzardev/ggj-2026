using System;
using System.Collections;
using UnityEngine;

public class WeaponProperties : MonoBehaviour
{
    [SerializeField] int _weaponDamage = 1;
    [SerializeField] int _maxBullet = 12;
    [SerializeField] protected int _currentBulletAmount;
    [SerializeField] float _reloadTime = 1.5f;
    [SerializeField] float _shootRate = 0.5f;
    
    public int WeaponDamage => _weaponDamage;
    public event Action<int, int> OnAmmoChanged;

    private void Start()
    {
        _currentBulletAmount = _maxBullet;
        OnAmmoChanged?.Invoke(_currentBulletAmount, _maxBullet);
    }
    public virtual void ShootWeapon()
    {
        if (_currentBulletAmount <= 0)
        {
            Debug.Log("Out of Ammo, Reload!");
            StartCoroutine(ReloadWeapon());
            return;
        }
        _currentBulletAmount--;
        OnAmmoChanged?.Invoke(_currentBulletAmount, _maxBullet);

    }

    protected IEnumerator ReloadWeapon()
    {
        Debug.Log("Reloading Weapon");
        //insert reloading anim here
        yield return new WaitForSeconds(_reloadTime);
        _currentBulletAmount = _maxBullet;
        OnAmmoChanged?.Invoke(_currentBulletAmount, _maxBullet);
    }

}
