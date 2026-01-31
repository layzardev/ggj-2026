using System;
using System.Collections;
using UnityEngine;

public class WeaponProperties : MonoBehaviour
{
    [SerializeField] int _weaponDamage = 1;
    [SerializeField] int _maxBullet = 12;
    [SerializeField] protected int _currentBulletAmount;
    [SerializeField] protected float _reloadTime = 1.5f;
    [SerializeField] protected float _shootDelay = 0.3f;
    
    public int WeaponDamage => _weaponDamage;
    public event Action<int, int> OnAmmoChanged;

    protected bool _isReloading = false;

    protected float _delayTimer = 0;

    private void Start()
    {
        _currentBulletAmount = _maxBullet;
        OnAmmoChanged?.Invoke(_currentBulletAmount, _maxBullet);
    }

    private void Update()
    {
        if (_delayTimer >= 0)
        {
            _delayTimer -= Time.deltaTime;
        }
    }

    public virtual void ShootWeapon()
    {
        _delayTimer = _shootDelay;
        _currentBulletAmount--;
        OnAmmoChanged?.Invoke(_currentBulletAmount, _maxBullet);
        return;

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
