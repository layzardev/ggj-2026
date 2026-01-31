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
        Debug.Log("Checking");
        foreach (Transform child in cardParent)
            Destroy(child.gameObject);
        Debug.Log("Showing power-up UI with " + cards.Count + " cards.");
        foreach (var card in cards)
        {
            var btn = Instantiate(cardPrefab, cardParent);
            btn.Setup(card, onSelect);
        }
        Debug.Log("Showing power-up UI after instantiating with " + cards.Count + " cards.");
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
