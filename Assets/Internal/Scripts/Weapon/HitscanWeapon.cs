using UnityEngine;

public class HitscanWeapon : WeaponProperties
{
    [Header("Hitscan Properties")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] GameObject trailPrefab;

    [Header("Stats")]
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 10;

    public override void ShootWeapon()
    {
        Debug.Log("Shooting Hitscan Weapon");
        Vector3 origin = cameraTransform.position;
        Vector3 direction = cameraTransform.forward;

        Vector3 hitPoint = origin + direction * range;

        if (_currentBulletAmount <= 0)
        {
            Debug.Log("Out of Ammo, Reload!");
            StartCoroutine(ReloadWeapon());
            return;
        }
        _currentBulletAmount--;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);
            hitPoint = hit.point;

            if (hit.collider.TryGetComponent<EnemyProperties>(out var enemy))
            {
                Debug.Log("Hit enemy: " + enemy.name);
                enemy.TakeDamage();
            }
        }
    }
}
