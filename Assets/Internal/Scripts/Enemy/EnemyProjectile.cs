using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    int _projectileDamage;
    EnemyProperties _parentEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _projectileDamage = _parentEnemy != null ? _parentEnemy.EnemyDamage : 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetProjectileDamage()
    {
        return _projectileDamage;
    }

    public void InitializeProjectile(EnemyProperties parentEnemy, Vector3 direction)
    {
        _parentEnemy = parentEnemy;
        Rigidbody rb = GetComponent<Rigidbody>();
        float projectileSpeed = 10f;
        rb.linearVelocity = direction.normalized * projectileSpeed;
        Destroy(gameObject, 5f); // Destroy the projectile after 5 seconds to avoid clutter
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerProperties player = collision.gameObject.GetComponent<PlayerProperties>();
            if (player != null)
            {
                player.TakeDamage(GetProjectileDamage());
            }
            Destroy(gameObject); // Destroy the projectile upon hitting the player
        }
        else if (!collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the projectile if it hits anything other than an enemy
            Destroy(gameObject);
        }
    }
}
