using UnityEngine;

public class _TapToSwitch : MonoBehaviour
{
    [Header("UI Groups")]
    [SerializeField] private GameObject titlescreenPage;
    [SerializeField] private GameObject mainMenuPage;

    private bool isInTitle = true;

    private void Start()
    {
        ShowTitle();
    }

    // Dipanggil saat layar ditap
    public void OnScreenTap()
    {
        // Toggle state
        isInTitle = !isInTitle;

        if (isInTitle)
            ShowTitle();
        else
            ShowMainMenu();
    }

    private void ShowTitle()
    {
        titlescreenPage.SetActive(true);
        mainMenuPage.SetActive(false);
    }

    private void ShowMainMenu()
    {
        titlescreenPage.SetActive(false);
        mainMenuPage.SetActive(true);
    }
}
