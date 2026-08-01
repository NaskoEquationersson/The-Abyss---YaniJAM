using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI Panelleri")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    private bool isPaused = false;

    void Update()
    {
        // Klavyeden ESC'ye basıldığında tetiklenir
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Eğer Ayarlar Paneli açıksa, ana ESC tuşu önce onu/içindekileri etkilesin
            if (settingsPanel != null && settingsPanel.activeSelf)
            {
                return; 
            }

            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SaveAndResume()
    {
        SaveGameData();
        ResumeGame();
    }

    public void SaveAndQuit()
    {
        SaveGameData();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
    }

    private void SaveGameData()
    {
        Debug.Log("Oyun verileri kaydedildi...");
    }
}