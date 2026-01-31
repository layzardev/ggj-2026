using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class _powerUpUI : MonoBehaviour
{
    public Transform cardParent;
    public _cardButton cardPrefab;
    public Button skipButton;

    private System.Action onSkipCallback;

    public void Show(List<_powerUpCard> cards, System.Action<_powerUpCard> onSelect, System.Action onSkip = null)
    {
        gameObject.SetActive(true);

        onSkipCallback = onSkip;

        foreach (Transform child in cardParent)
            Destroy(child.gameObject);

        foreach (var card in cards)
        {
            var btn = Instantiate(cardPrefab, cardParent);
            btn.Setup(card, onSelect);
        }

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(true);
            skipButton.onClick.RemoveAllListeners();
            skipButton.onClick.AddListener(() =>
            {
                onSkipCallback?.Invoke();
                Hide();
            });
        }
    }

    public void Hide()
    {
        // Hide button skip
        if (skipButton != null)
            skipButton.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }
}
