using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Animasyon Bilesenleri")]
    [SerializeField] private Animator targetAnimator;
    [SerializeField] private string animationTriggerName = "StartGame";

    [Header("UI Panelleri")]
    [SerializeField] private GameObject menuCanvas;     // Baslangic_Panel
    [SerializeField] private GameObject settingsPanel;  // esc_Panel / SETTINGS_panel
    [SerializeField] private SettingsManager settingsManager; // reference so we can set its return target

    void Awake()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    // "Başla" button — starts the game
    public void OnStartButtonClicked()
    {
        Debug.Log("Başla butonuna basıldı, animasyon tetikleniyor...");

        if (targetAnimator != null)
            targetAnimator.SetTrigger(animationTriggerName);

        if (menuCanvas != null)
            menuCanvas.SetActive(false);
    }

    // "Ayarlar" button — opens settings, tells SettingsManager to return to the main menu when closed
    public void OnSettingsButtonClicked()
    {
        Debug.Log("Ayarlar tıklandı.");

        if (menuCanvas != null) menuCanvas.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);

        if (settingsManager != null && menuCanvas != null)
        {
            settingsManager.SetReturnTarget(menuCanvas);
        }
    }
    public void OnContinueButtonClicked()
    {
        Debug.Log("Devam et tıklandı.");
        // I let it act as Start for now.
        if (targetAnimator != null)
            targetAnimator.SetTrigger(animationTriggerName);

        if (menuCanvas != null)
            menuCanvas.SetActive(false);
    }
}