using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] float lifeTime = 3f;

    int projectileDamage;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void InitializeProjectile(WeaponProperties parentWeapon, Vector3 direction)
    {
        projectileDamage = parentWeapon.WeaponDamage;

        rb.linearVelocity = direction.normalized * speed;

        Destroy(gameObject, lifeTime);
    }

    public int GetProjectileDamage()
    {
        return projectileDamage;
    }
}
