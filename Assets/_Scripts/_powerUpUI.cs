using UnityEngine;
using System.Collections.Generic;

public class _powerUpUI : MonoBehaviour
{

    public Transform cardParent;
    public _cardButton cardPrefab;

    public void Show(List<_powerUpCard> cards, System.Action<_powerUpCard> onSelect)
    {
        gameObject.SetActive(true);

        foreach (Transform child in cardParent)
            Destroy(child.gameObject);

        foreach (var card in cards)
        {
            var btn = Instantiate(cardPrefab, cardParent);
            btn.Setup(card, onSelect);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
