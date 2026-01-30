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

        var cards = GetRandomCards(3);
        powerUpUI.Show(cards, SelectCard);

        while (selectedCard == null)
            yield return null;

        Debug.Log("Selected Upgrade: " + selectedCard.cardName);

        powerUpUI.Hide();
        Time.timeScale = 1f;
    }

    private void SelectCard(_powerUpCard card)
    {
        selectedCard = card;
    }

    private List<_powerUpCard> GetRandomCards(int count)
    {
        List<_powerUpCard> temp = new(allCards);
        List<_powerUpCard> result = new();

        for (int i = 0; i < count && temp.Count > 0; i++)
        {
            int index = Random.Range(0, temp.Count);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }

        return result;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        CancelInvoke(); // safety net
    }
}
