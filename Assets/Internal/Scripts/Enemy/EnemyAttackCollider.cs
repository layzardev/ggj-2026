using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    public int damageAmount = 1;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("EnemyAttackCollider collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {            
            PlayerProperties.Instance.TakeDamage(damageAmount);
            Debug.Log("Player hit by enemy! Health reduced.");
            
        }
    }
}
