using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class _cardButton : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text desc;
    [SerializeField] private Image cardImage;

    private _powerUpCard card;
    private Action<_powerUpCard> onClick;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    public void Setup(_powerUpCard c, Action<_powerUpCard> callback)
    {
        if (c == null)
        {
            Debug.LogWarning("PowerUpCard is null!");
            return;
        }

        card = c;
        onClick = callback;

        if (title != null)
            title.text = c.cardName;

        if (desc != null)
            desc.text = c.description;

        if (cardImage != null && c.cardImage != null)
            cardImage.sprite = c.cardImage;
    }

    private void HandleClick()
    {
        if (card == null)
        {
            Debug.LogWarning("CardButton clicked but card is not set!");
            return;
        }

        onClick?.Invoke(card);
    }
}
