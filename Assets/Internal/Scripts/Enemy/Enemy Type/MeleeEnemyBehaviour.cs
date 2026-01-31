using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class MeleeEnemyBehaviour : EnemyProperties
{
    [SerializeField] float _attackDelay = 2f;

    Vector2 _playerDirection;
    float _step;
 
    Animator _attackAnimator;

    bool _isColliding;

    private void Start()
    {
        _attackAnimator = GetComponentInChildren<Animator>();

        target = PlayerProperties.Instance.gameObject;
    }

    private void Update()
    {
        _step = _enemySpeed * Time.deltaTime;
        if (target == null) return;
        transform.LookAt(target.transform);
        if (!_isColliding) transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _step);
        
    }
    
    private void Attack()
    {
        PlayerProperties.Instance.TakeDamage(_enemyDamage);
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            if (_isColliding)
            {
                Attack();
            }
           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isColliding = true;
            
            StartCoroutine(AttackRoutine());

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isColliding = false;
            StopAllCoroutines();
        }
    }
}



