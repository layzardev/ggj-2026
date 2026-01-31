using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerProperties : Singleton<PlayerProperties>
{
    [SerializeField] int _playerHealth = 100;
    [SerializeField] int _playerScore = 0;
    [SerializeField] int _playerLevel = 1;
    [SerializeField] float _playerSpeed = 1;
    [SerializeField] float _playerJumpHeight = 1;

    [SerializeField] WeaponProperties _weapon;
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

    public void TakeDamage(int value)
    {
        _playerHealth -= value;
        OnHealthChanged?.Invoke(_playerHealth);
        if (_playerHealth <= 0)
        {
            PlayerDeath();
            GameManager.Instance.OnPlayerDeath?.Invoke();
        }
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

    
}


