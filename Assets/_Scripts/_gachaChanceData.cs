using UnityEngine;

public class _gachaChanceData
{
    public int normal = 60;
    public int advanced = 30;
    public int legend = 10;

    public void Normalize()
    {
        normal = Mathf.Max(0, normal);
        advanced = Mathf.Max(0, advanced);
        legend = Mathf.Max(0, legend);

        int total = normal + advanced + legend;

        if (total == 0)
        {
            legend = 100;
            return;
        }

        float factor = 100f / total;

        normal = Mathf.RoundToInt(normal * factor);
        advanced = Mathf.RoundToInt(advanced * factor);
        legend = 100 - normal - advanced; // pastikan tepat 100
    }

    public _cardRarity RollRarity()
    {
        Normalize(); // pastikan chance valid

        int roll = UnityEngine.Random.Range(0, 100);
        if (roll < normal)
            return _cardRarity.Common;
        else if (roll < normal + advanced)
            return _cardRarity.Advanced;
        else
            return _cardRarity.Legend;
    }
}

