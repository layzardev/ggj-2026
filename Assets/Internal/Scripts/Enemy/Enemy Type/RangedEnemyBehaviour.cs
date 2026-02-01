using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class RangedEnemyBehaviour : EnemyProperties
{
    [SerializeField] float _attackDelay = 2f;
    [SerializeField] GameObject _enemyProjectilePrefab;

    Vector2 _playerDirection;
    float _step;

    Animator _attackAnimator;

    bool _isColliding;

    float _distanceToPlayer;
    [SerializeField] float _attackRange = 5f;

    private void Start()
    {
        _attackAnimator = GetComponentInChildren<Animator>();
        _distanceToPlayer = 0;
        target = PlayerProperties.Instance.gameObject;
        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        _step = _enemySpeed * Time.deltaTime;
        if (target == null) return;
        transform.LookAt(target.transform);
        _distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
        if (_distanceToPlayer > _attackRange) transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _step);

    }

    private void Attack()
    {
        Instantiate(_enemyProjectilePrefab, transform.position + transform.forward * 1.5f, transform.rotation)
            .GetComponent<EnemyProjectile>().InitializeProjectile(this, this.transform.forward);
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            if (_distanceToPlayer < _attackRange)
            {
                Attack();
            }

        }
    }

   
}
