using UnityEngine;

public abstract class EnemyProperties : MonoBehaviour
{
    [SerializeField] protected int _enemyHealth = 1;
    [SerializeField] protected float _enemySpeed = 1;
    [SerializeField] protected int _enemyDamage = 10;
    [SerializeField] protected int _expDrop = 1;

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

    public void TakeDamage(int value = 1)
    {
        _enemyHealth -= value;
        if (_enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }
    public void EnemyDeath()
    {
        GameManager.Instance?.ModifyEnemyCount(1);
        GameManager.Instance?.OnEnemyDeath?.Invoke();
       
        PlayerProperties.Instance?.ModifyScore(_expDrop);
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
