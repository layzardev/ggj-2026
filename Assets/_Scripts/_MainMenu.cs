using UnityEngine;
using UnityEngine.SceneManagement;

public class _MainMenu : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    private void Start()
    {
        CloseAllPanels();
    }

    // === PLAY ===
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // ================= SETTINGS =================
    public void Settings()
    {
        if (settingsPanel == null) return;

        bool isActive = settingsPanel.activeSelf;

        CloseAllPanels();
        settingsPanel.SetActive(!isActive);
    }

    // ================= CREDITS =================
    public void Credits()
    {
        if (creditsPanel == null) return;

        bool isActive = creditsPanel.activeSelf;

        CloseAllPanels();
        creditsPanel.SetActive(!isActive);
    }

    // ================= CLOSE ALL =================
    public void CloseAllPanels()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }


    // === EXIT ===
    public void ExitGame()
    {
        Debug.Log("Exit Game");

        // Jika di build
        Application.Quit();

        // Jika di Editor (biar keliatan efeknya)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
