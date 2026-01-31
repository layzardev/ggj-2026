using UnityEngine;

public abstract class EnemyProperties : MonoBehaviour
{
    [SerializeField] protected int _enemyHealth = 1;
    [SerializeField] protected float _enemySpeed = 1;
    [SerializeField] protected int _enemyDamage = 10;

    protected GameObject target;
    public int EnemyDamage => _enemyDamage;
    void Start()
    {
        target = PlayerProperties.Instance.gameObject;
    }

    void Update()
    {
        transform.LookAt(target.transform);
    }

    public void TakeDamage()
    {
        _enemyHealth--;
        if (_enemyHealth <= 0)
        {
            EnemyDeath();
        }
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
