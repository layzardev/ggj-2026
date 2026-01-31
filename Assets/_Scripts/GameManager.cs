using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public _powerUpUI powerUpUI;
    public List<_powerUpCard> allCards;

    private _powerUpCard selectedCard;
    private int wave = 1;
    private Coroutine waveRoutine;

    public PlayerStats player;


    private void Start()
    {
        waveRoutine = StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            Debug.Log($"Wave {wave} Start");

            // Simulasi wave
            yield return new WaitForSeconds(3f);

            yield return StartCoroutine(PowerUpPhase());

            wave++;
        }
    }

    private IEnumerator PowerUpPhase()
    {
        Time.timeScale = 0f;

        selectedCard = null;

        var cards = GetGachaCards(3);
        powerUpUI.Show(cards, SelectCard, SkipCard);

        while (selectedCard == null)
            yield return null;

        if (selectedCard != null)
            Debug.Log("Selected: " + selectedCard.cardName);

        powerUpUI.Hide();
        Time.timeScale = 1f;
    }

    private void SkipCard()
    {
        Debug.Log("PowerUp Skipped!");
        // selectedCard = new _powerUpCard(); // dummy supaya loop berhenti

        selectedCard = ScriptableObject.CreateInstance<_powerUpCard>();
        selectedCard.cardName = "Skipped";
        Time.timeScale = 1f;
    }


    private void SelectCard(_powerUpCard card)
    {
        selectedCard = card;

        // player.ApplyPowerUp(card);
    }


    _powerUpCard GetRandomCardByRarity(_cardRarity rarity)
    {
        List<_powerUpCard> pool = allCards.FindAll(c => c.rarity == rarity);

        if (pool.Count == 0 && rarity != _cardRarity.Common)
            pool = allCards.FindAll(c => c.rarity == _cardRarity.Common);

        if (pool.Count == 0)
            return null;

        return pool[Random.Range(0, pool.Count)];
    }

    List<_powerUpCard> GetGachaCards(int count)
    {
        List<_powerUpCard> result = new();

        int safety = 0; // anti infinite loop

        while (result.Count < count && safety < 100)
        {
            safety++;

            _cardRarity rarity = _gachaSystem.RollRarity();
            _powerUpCard card = GetRandomCardByRarity(rarity);

            if (card != null)
                result.Add(card);
        }

        return result;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        CancelInvoke();
    }
}
