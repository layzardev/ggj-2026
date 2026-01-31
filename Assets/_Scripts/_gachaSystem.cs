using UnityEngine;

public class _gachaSystem : MonoBehaviour
{
    public static _cardRarity RollRarity()
    {
        int roll = Random.Range(0, 100);

        if (roll < 60)
            return _cardRarity.Common;      // 60%
        else if (roll < 90)
            return _cardRarity.Advanced;    // 30%
        else
            return _cardRarity.Legend;      // 10%
    }
}
