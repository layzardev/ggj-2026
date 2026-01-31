using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class MeleeEnemyBehaviour : EnemyProperties
{
    Vector2 PlayerDirection;
    float step;


    private void Update()
    {
        step = _enemySpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        transform.LookAt(target.transform);
    }

}
