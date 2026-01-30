using UnityEngine;

public abstract class EnemyProperties : MonoBehaviour
{
    [SerializeField] int _enemyHealth = 1;
    [SerializeField] float _enemySpeed = 1;
    [SerializeField] int _enemyDamage = 10;


    public int EnemyDamage => _enemyDamage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            _enemyHealth -= other.GetComponent<PlayerProjectile>().GetProjectileDamage();
            if (_enemyHealth <= 0)
            {
                EnemyDeath();
            }
        }
    }
}
