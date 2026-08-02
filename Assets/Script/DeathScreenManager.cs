using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    public static DeathScreenManager Instance;
    public GameObject deathPanel;

    void Awake()
    {
        Instance = this;
        if (deathPanel != null) deathPanel.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        if (deathPanel != null) deathPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}