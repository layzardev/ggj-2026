using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float _timeElapsed;
    

    int targetEnemiesThisWave = 6;
    int enemyAdditionEachWave = 10;

    int _existingEnemies;


    int _enemiesKilled;
    int _wavesCompleted;


    public _powerUpUI powerUpUI;
    public List<_powerUpCard> allCards;

    public bool isGameOver = false;
    public bool IsPaused = false;
    private _powerUpCard selectedCard;
    private int wave = 1;
    private Coroutine waveRoutine;

    public PlayerProperties player;

    public int EnemiesKilled => _enemiesKilled;
    public int TargetEnemiesThisWave => targetEnemiesThisWave; 


    public Action OnPlayerDeath;
    public Action OnEnemyDeath;
    public Action OnTargetChanged;
    public Action OnPause;

    private void Start()
    {
        waveRoutine = StartCoroutine(WaveLoop());
    }
    private void Update()
    {
        _timeElapsed += Time.deltaTime;
    }

    private void OnEnable()
    {
        OnPlayerDeath += HandlePlayerDeath;
        //OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        OnPlayerDeath -= HandlePlayerDeath;
        //OnEnemyDeath -= HandleEnemyDeath;
        StopAllCoroutines();
        CancelInvoke();
    }

     void HandlePlayerDeath()
    {
        // Handle game over logic here
        Debug.Log("Player has died. Game Over.");
        isGameOver = true;
        Time.timeScale = 0f;
        
    }

    //void HandleEnemyDeath()
    //{
    //    _existingEnemies -= 1;
    //    _enemiesKilled += 1;
    //    Debug.Log("An enemy has died. Remaining enemies: " + _existingEnemies);
    //}

    public void ModifyEnemyCount(int amount)
    {
        _existingEnemies -= amount;
        _enemiesKilled += amount;
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            Debug.Log($"Wave {wave} Start");

            // Simulasi wave
            //yield return new WaitForSeconds(3f);

            

            if (_enemiesKilled >= targetEnemiesThisWave)
            {
                yield return StartCoroutine(PowerUpPhase());
                wave++;
                enemyAdditionEachWave += 3;
                targetEnemiesThisWave += enemyAdditionEachWave;
                OnTargetChanged?.Invoke();

            }

            yield return null;
        }
    }

    private IEnumerator PowerUpPhase()
    {
        Time.timeScale = 0f;
        //DestroyExistingEnemies(); //tambahan 
        IsPaused = true;
        selectedCard = null;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        var cards = GetGachaCards(3);
        powerUpUI.Show(cards, SelectCard, SkipCard);

        while (selectedCard == null)
            yield return null;

        if (selectedCard != null)
            Debug.Log("Selected: " + selectedCard.cardName);

        powerUpUI.Hide();
        IsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DestroyExistingEnemies()
    {
        EnemyProperties[] enemies = FindObjectsOfType<EnemyProperties>();
        foreach (EnemyProperties enemy in enemies)
        {
            enemy.EnemyDeath(); 
        }
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

        player.ApplyPowerUp(card);
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        OnPause?.Invoke();
    }

    _powerUpCard GetRandomCardByRarity(_cardRarity rarity)
    {
        List<_powerUpCard> pool = allCards.FindAll(c => c.rarity == rarity);

        if (pool.Count == 0 && rarity != _cardRarity.Common)
            pool = allCards.FindAll(c => c.rarity == _cardRarity.Common);

        if (pool.Count == 0)
            return null;

        return pool[UnityEngine.Random.Range(0, pool.Count)];
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


}
