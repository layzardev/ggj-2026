using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float _timeElapsed;
    float _existingEnemies;

    public Action OnPlayerDeath;
    public Action OnEnemyDeath;

    private void OnEnable()
    {
        OnPlayerDeath += HandlePlayerDeath;
        OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        OnPlayerDeath -= HandlePlayerDeath;
        OnEnemyDeath -= HandleEnemyDeath;
    }

     void HandlePlayerDeath()
    {
        // Handle game over logic here
        Debug.Log("Player has died. Game Over.");
    }

    void HandleEnemyDeath()
    {
        _existingEnemies--;
        Debug.Log("An enemy has died. Remaining enemies: " + _existingEnemies);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
