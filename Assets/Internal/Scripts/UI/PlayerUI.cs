using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class PlayerUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] 
    private Slider healthBar;
    [SerializeField] 
    private TextMeshProUGUI healthText;
    [SerializeField] 
    private TextMeshProUGUI ammoText;
    [SerializeField] 
    private TextMeshProUGUI scoreText;

    PlayerProperties player;
    WeaponProperties weapon;

    private void Awake()
    {
        player = GetComponentInParent<PlayerProperties>();
        weapon = player.PlayerWeapon;
    }

    private void Start()
    {
        // Initialize health bar and text
        if (player != null && healthBar != null)
        {
            healthBar.maxValue = player.PlayerHealth; // set max
            healthBar.value = player.PlayerHealth;    // current value
            healthText.text = $"HP: {player.PlayerHealth}";
        }
    }
    
    private void OnEnable()
    {
        if (player != null)
            player.OnHealthChanged += UpdateHealth;
            player.OnScoreChanged += UpdateScore;

        if (weapon != null)
            weapon.OnAmmoChanged += UpdateAmmo;
    }

    private void OnDisable()
    {
        if (player != null)
            player.OnHealthChanged -= UpdateHealth;
            player.OnScoreChanged -= UpdateScore;

        if (weapon != null)
            weapon.OnAmmoChanged -= UpdateAmmo;
    }

    void UpdateHealth(int value)
    {
        if (healthText != null)
            healthText.text = $"HP: {value}";
        if (healthBar != null)
            healthBar.value = value;
    }

    void UpdateAmmo(int current, int max)
    {
        if (ammoText != null)
            ammoText.text = $"{current} / {max}";
    }

    private void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}
