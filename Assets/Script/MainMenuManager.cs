using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Animasyon Bilesenleri")]
    // Sinyali göndereceğimiz nesnenin Animator'ı (Örn: PlayerAnimationBody veya Kamera)
    [SerializeField] private Animator targetAnimator; 
    [SerializeField] private string animationTriggerName = "StartGame";

    [Header("UI Paneli")]
    [SerializeField] private GameObject menuCanvas; // Butonların olduğu Canvas

    // BAŞLA Butonuna tıklandığında çalışacak fonksiyon
    public void OnStartButtonClicked()
    {
        Debug.Log("Başla butonuna basıldı, animasyon tetikleniyor...");

        // 1. Animasyon sinyalini (Trigger) gönder
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger(animationTriggerName);
        }

        // 2. Butonların olduğu ekranı gizle (İsteğe bağlı)
        if (menuCanvas != null)
        {
            menuCanvas.SetActive(false);
        }
    }

    // DEVAM ET Butonuna tıklandığında
    public void OnContinueButtonClicked()
    {
        Debug.Log("Devam et tıklandı.");
        // Buraya devam et mantığı (Kayıtlı oyunu yükleme vs.) gelecek
    }

    // AYARLAR Butonuna tıklandığında
    public void OnSettingsButtonClicked()
    {
        Debug.Log("Ayarlar tıklandı.");
    }
}