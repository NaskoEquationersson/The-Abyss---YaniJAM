using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI Panelleri")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private SettingsManager settingsManager;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel != null && settingsPanel.activeSelf)
            {
                // Settings is open — let SettingsManager handle this Escape press, not us
                if (settingsManager != null) settingsManager.OnEscClicked();
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
        SceneManager.LoadScene("MainScene");
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
            settingsPanel.SetActive(true);

            if (settingsManager != null && pauseMenuPanel != null)
            {
                settingsManager.SetReturnTarget(pauseMenuPanel);
            }
        }
    }

    private void SaveGameData()
    {
        Debug.Log("Oyun verileri kaydedildi...");
    }
}