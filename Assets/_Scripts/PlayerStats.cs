using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int attack;
    public int defense;
    public int speed;
    public int health;

    public void ApplyPowerUp(_powerUpCard card)
    {
        switch (card.rarity)
        {
            case _cardRarity.Common:
                attack += 2;
                defense += 2;
                break;

            case _cardRarity.Advanced:
                attack += 8;
                defense -= 2;
                break;

            case _cardRarity.Legend:
                attack += 15;
                defense -= 2;
                speed -= 2;
                health -= 2;
                break;
        }
    }
}