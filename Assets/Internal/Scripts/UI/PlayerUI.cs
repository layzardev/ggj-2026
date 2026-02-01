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
    [SerializeField]
    private TextMeshProUGUI enemyCounterText;
    [SerializeField]
    private TextMeshProUGUI targetCounterText;
    [SerializeField]
    private GameObject EndScreen;
    [SerializeField]
    private TextMeshProUGUI EndScore;
    [SerializeField]
    private TextMeshProUGUI EndKill;

    public GameObject PauseMenu;
    //[SerializeField]
    //private TextMeshProUGUI comboText;

    PlayerProperties player;
    WeaponProperties weapon;
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        player = PlayerProperties.Instance;
        weapon = player.PlayerWeapon;
    }

    private void Start()
    {
        
        // Initialize health bar and text
        if (player != null && healthBar != null)
        {
            healthBar.maxValue = player.MaxPlayerHealth; // set max
            healthBar.value = player.PlayerHealth;    // current value
            healthText.text = $"HP: {player.PlayerHealth}";

            if (gameManager != null)
            {
                enemyCounterText.text = "0";
                targetCounterText.text = gameManager.TargetEnemiesThisWave.ToString();
            }
        }
    }

    private void OnEnable()
    {
        if (player != null)
        {
            player.OnHealthChanged += UpdateHealth;
            player.OnScoreChanged += UpdateScore;
            player.OnMaxHealthChanged += UpdateMaxHealth;
        }

        if (weapon != null)
        {
            weapon.OnAmmoChanged += UpdateAmmo;
        }

        if (gameManager != null)
        {
            gameManager.OnEnemyDeath += UpdateEnemyCounter;
            gameManager.OnTargetChanged += UpdateTargetCounter;
            gameManager.OnPause += TogglePauseUI;
            gameManager.OnPlayerDeath += ShowEndScreen;
            
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.OnHealthChanged -= UpdateHealth;
            player.OnScoreChanged -= UpdateScore;
            player.OnMaxHealthChanged -= UpdateMaxHealth;
        }

        if (weapon != null)
        {
            weapon.OnAmmoChanged -= UpdateAmmo;
        }

        if (gameManager != null)
        {
            gameManager.OnEnemyDeath -= UpdateEnemyCounter;
            gameManager.OnTargetChanged -= UpdateTargetCounter;
            gameManager.OnPause -= TogglePauseUI;
            gameManager.OnPlayerDeath -= ShowEndScreen;
        }
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

    private void UpdateEnemyCounter()
    {
        if (enemyCounterText != null)
            //Debug.Log("Updating enemy counter text. " + gameManager._enemiesKilled.ToString() + " " + gameManager._enemiesKilled);
            enemyCounterText.text = gameManager.EnemiesKilled.ToString();
    }

    private void UpdateTargetCounter()
    {
        if (targetCounterText != null)
            targetCounterText.text = gameManager.TargetEnemiesThisWave.ToString();
    }

    private void UpdateMaxHealth(int value)
    {
        if (healthBar != null)
            healthBar.maxValue = value;
    }

    public void TogglePauseUI()
    {
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
        }
    }

    private void ShowEndScreen()
    {
        if (EndScreen != null)
        {
            EndScreen.SetActive(true);
            if (EndScore != null)
            {
                EndScore.text = $"Final Score: {player.PlayerScore}";
            }
            if (EndKill != null)
            {
                EndKill.text = $"Total Enemies Defeated: {gameManager.EnemiesKilled}";
            }
        }
    }


}
