using UnityEngine;

public class WeaponProperties : MonoBehaviour
{
    [SerializeField] int _weaponDamage = 1;
    [SerializeField] int _maxBullet = 12;
    [SerializeField] int _currentBullet;
    [SerializeField] float _reloadTime = 1.5f;
    [SerializeField] float _shootRate = 0.5f;

    public int WeaponDamage => _weaponDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
