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

    bool _isReloading = false;

    private void Start()
    {
        _currentBulletAmount = _maxBullet;
        OnAmmoChanged?.Invoke(_currentBulletAmount, _maxBullet);
    }
    public virtual void ShootWeapon()
    {
        if (_isReloading)
        {
            Debug.Log("Currently Reloading...");
            return;
        }
        if (_currentBulletAmount <= 0 )
        {
            Debug.Log("Out of Ammo, Reload!");
            StartCoroutine(ReloadWeapon());
            return;
        }
        _currentBulletAmount--;
        OnAmmoChanged?.Invoke(_currentBulletAmount, _maxBullet);

    }

    public void InitiateReload()
    {

        StartCoroutine(ReloadWeapon());
        // Add animations or sound effects here
    }

    public IEnumerator ReloadWeapon()
    {
        PlayerProperties.Instance.notificationText.gameObject.SetActive(true);
        PlayerProperties.Instance.notificationText.SetText("Reloading...");

        Debug.Log("Reloading Weapon");
        _isReloading = true;
        //insert reloading anim here
        yield return new WaitForSeconds(_reloadTime);
        _currentBulletAmount = _maxBullet;
        OnAmmoChanged?.Invoke(_currentBulletAmount, _maxBullet);
        _isReloading = false;

        PlayerProperties.Instance.notificationText.gameObject.SetActive(false);
        PlayerProperties.Instance.notificationText.SetText("");
    }

}
