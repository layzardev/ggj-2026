using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    [SerializeField]
    private RectTransform healthBar;

    public void setHealth(float health){
        Health = health;
        // healthBar.width = (Health/MaxHealth);
    }
}
