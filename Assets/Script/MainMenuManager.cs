using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Animasyon Bilesenleri")]
    [SerializeField] private Animator targetAnimator; 
    [SerializeField] private string animationTriggerName = "StartGame";

    [Header("UI Panelleri")]
    [SerializeField] private GameObject menuCanvas;     // Baslangic_Panel
    [SerializeField] private GameObject settingsPanel;  // esc_Panel / SETTINGS_panel

    void Awake()
    {
        // Sahne başında ayarlar panelinin kapalı olduğundan emin ol
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        Debug.Log("Başla butonuna basıldı, animasyon tetikleniyor...");

        if (targetAnimator != null)
            targetAnimator.SetTrigger(animationTriggerName);

        if (menuCanvas != null)
            menuCanvas.SetActive(false);
    }

    public void OnContinueButtonClicked()
    {
        Debug.Log("Devam et tıklandı.");
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Ayarlar tıklandı.");
        if (menuCanvas != null) menuCanvas.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }
}