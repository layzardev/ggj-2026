using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    int _projectileDamage;
    WeaponProperties _parentWeapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _projectileDamage = _parentWeapon != null ? _parentWeapon.WeaponDamage : 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetProjectileDamage()
    {
        return _projectileDamage;
    }
}
