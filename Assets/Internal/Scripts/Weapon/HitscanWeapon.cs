using System.Collections;
using UnityEngine;

public class HitscanWeapon : WeaponProperties
{
    [Header("Hitscan Properties")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] GameObject trailPrefab;
    [SerializeField] GameObject _hitMarker;

    [Header("Stats")]
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 10;

    public override void ShootWeapon()
    {
        Debug.Log("Shooting Hitscan Weapon");
        Vector3 origin = cameraTransform.position;
        Vector3 direction = cameraTransform.forward;

        Vector3 hitPoint = origin + direction * range;


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

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);
            hitPoint = hit.point;

            if (hit.collider.TryGetComponent<EnemyBodyPart>(out var enemyPart) )
            {
                
                if (enemyPart.bodyPartType == BodyPartType.Weakspot)
                {
                    StartCoroutine(ActivateMarker(0.25f));
                    Debug.Log("Critical hit: " + enemyPart.enemyProperties.name);
                    enemyPart.enemyProperties.TakeDamage(2*WeaponDamage);
                } else 
                {
                    Debug.Log("Normal hit: " + enemyPart.enemyProperties.name);
                    enemyPart.enemyProperties.TakeDamage(WeaponDamage);
                }
                    
            }
        }
    }

    IEnumerator ActivateMarker(float length)
    {
        _hitMarker.SetActive(true);
        yield return new WaitForSeconds(length);
        _hitMarker.SetActive(false);
    }
}
