using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/Card")]
public class _powerUpCard : ScriptableObject
{
    public Sprite cardImage;
    public string cardName;
    [TextArea] public string description;

    public _cardRarity rarity;

    // stats
    public int damage;
    public int maxHealth;
    public int magSize;
    public int reloadTime;    
    public int movementSpeed;
    public int recoil;
}
