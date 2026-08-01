using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("Paneller / Gruplar")]
    [SerializeField] private GameObject mainSettingsGroup; // Müzikler ve Kontroller butonlarının grubu
    [SerializeField] private GameObject slidersContainer;  // Slider'ların (Müzik ayarlarının) durduğu grup
    [SerializeField] private GameObject controllerContainer; // YENİ: Kontrol tuşları bilgisinin durduğu grup

    [Header("Geri Dönülecek Ekran")]
    [SerializeField] private GameObject returnTarget; // Ana menüden açıldıysa Baslangic_Panel, pause'dan açıldıysa pauseMenuPanel

    void OnEnable()
    {
        // Ayarlar Paneli her açıldığında varsayılan olarak Ana Ayarlar görünür olsun, diğerleri gizlensin
        ResetToMainSettings();
    }

    // "MÜZİKLER" Butonuna bağlanacak fonksiyon
    public void OpenAudioDetails()
    {
        if (mainSettingsGroup != null) mainSettingsGroup.SetActive(false);
        if (slidersContainer != null) slidersContainer.SetActive(true);
        if (controllerContainer != null) controllerContainer.SetActive(false);
    }

    // "KONTROLLER" Butonuna bağlanacak YENİ fonksiyon
    public void OpenControllerDetails()
    {
        if (mainSettingsGroup != null) mainSettingsGroup.SetActive(false);
        if (slidersContainer != null) slidersContainer.SetActive(false);
        if (controllerContainer != null) controllerContainer.SetActive(true);
    }

    // Sol üstteki görsel ESC Butonuna veya Klavyedeki ESC'ye basılınca çalışacak fonksiyon
    public void OnEscClicked()
    {
        // Eğer Slider'lar veya Controller ekranı açıksa: önce onları kapatıp ana butonları geri getir
        if ((slidersContainer != null && slidersContainer.activeSelf) ||
            (controllerContainer != null && controllerContainer.activeSelf))
        {
            if (slidersContainer != null) slidersContainer.SetActive(false);
            if (controllerContainer != null) controllerContainer.SetActive(false);
            if (mainSettingsGroup != null) mainSettingsGroup.SetActive(true);
        }
        // Eğer zaten Ana Ayarlar ekranındaysa: Ayarlar Panelini tamamen kapat ve geldiği ekrana dön
        else
        {
            gameObject.SetActive(false);
            if (returnTarget != null) returnTarget.SetActive(true);
        }
    }

    public void ResetToMainSettings()
    {
        if (mainSettingsGroup != null) mainSettingsGroup.SetActive(true);
        if (slidersContainer != null) slidersContainer.SetActive(false);
        if (controllerContainer != null) controllerContainer.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscClicked();
        }
    }
}