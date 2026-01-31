using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerProperties : Singleton<PlayerProperties>
{
    [SerializeField] int _maxPlayerHealth = 10;
    [SerializeField] int _playerHealth;

    [SerializeField] int _playerScore = 0;
    [SerializeField] int _playerLevel = 1;
    [SerializeField] float _playerSpeed = 1;
    [SerializeField] float _playerJumpHeight = 1;

    [SerializeField] WeaponProperties _weapon;

    public int MaxPlayerHealth => _maxPlayerHealth;
    public int PlayerHealth => _playerHealth;
    public int PlayerScore => _playerScore;
    public int PlayerLevel => _playerLevel;
    public float PlayerSpeed => _playerSpeed;
    public WeaponProperties PlayerWeapon => _weapon;
    public float PlayerJumpHeight => _playerJumpHeight;
    public bool disableAcceleration = false;
    public bool isAttacking = false;
    public bool isSliding = false;

    public TextMeshProUGUI notificationText;

    PlayerInput _playerInput;

    public Action OnJump;
    public event Action<int> OnHealthChanged;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnLevelChanged;

    public event Action<int> OnMaxHealthChanged;

    void Start()
    {
        
        _playerHealth = _maxPlayerHealth;
    }

    public void TakeDamage(int value)
    {
        _playerHealth -= value;
        OnHealthChanged?.Invoke(_playerHealth);
        if (_playerHealth <= 0)
        {
            PlayerDeath();
            GameManager.Instance.OnPlayerDeath?.Invoke();
        }
        StartCoroutine(ShowNotification("Took " + value + " damage!", 2f));
    }

    public IEnumerator ShowNotification(string message, float duration)
    {
        notificationText.SetText(message);
        notificationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        notificationText.gameObject.SetActive(false);
    }
    void PlayerDeath()
    {

        Destroy(gameObject);
    }

    public void ModifyScore(int value)
    {
        _playerScore += value;
        OnScoreChanged?.Invoke(_playerScore);
    }

    public void ModifyHealth(int value)
    {
        _playerHealth += value;
        OnHealthChanged?.Invoke(_playerHealth);
    }

    public void ModifyLevel(int value)
    {
        _playerLevel += value;
        OnLevelChanged?.Invoke(_playerLevel);
    }

    public void ModifySpeed(float value)
    {
        _playerSpeed += value;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            _playerHealth -= other.GetComponent<EnemyProjectile>().GetProjectileDamage();
            OnHealthChanged?.Invoke(_playerHealth);
            if (_playerHealth <= 0)
            {
                PlayerDeath();
            }
        }
    }

    public void ApplyPowerUp(_powerUpCard card)
    {
        switch (card.rarity)
        {
            case _cardRarity.Common:
                _weapon.ModifyDamage(2);
                _maxPlayerHealth += 5;
                break;

            case _cardRarity.Advanced:
                _weapon.ModifyDamage(4);
                _maxPlayerHealth += 7;
                break;

            case _cardRarity.Legend:
                _weapon.ModifyDamage(8);
                _maxPlayerHealth += 10;
                break;
        }
        OnMaxHealthChanged?.Invoke(_maxPlayerHealth);


    }

}


