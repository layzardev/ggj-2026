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
}
