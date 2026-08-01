using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("Paneller / Gruplar")]
    [SerializeField] private GameObject mainSettingsGroup;
    [SerializeField] private GameObject slidersContainer;
    [SerializeField] private GameObject controllerContainer;

    [Header("Geri Dönülecek Ekran")]
    [SerializeField] private GameObject returnTarget; // default fallback; can be overridden at runtime

    void OnEnable()
    {
        ResetToMainSettings();
    }

    // Call this from whichever menu opens Settings, so it knows where to return
    public void SetReturnTarget(GameObject target)
    {
        returnTarget = target;
    }

    public void OpenAudioDetails()
    {
        if (mainSettingsGroup != null) mainSettingsGroup.SetActive(false);
        if (slidersContainer != null) slidersContainer.SetActive(true);
        if (controllerContainer != null) controllerContainer.SetActive(false);
    }

    public void OpenControllerDetails()
    {
        if (mainSettingsGroup != null) mainSettingsGroup.SetActive(false);
        if (slidersContainer != null) slidersContainer.SetActive(false);
        if (controllerContainer != null) controllerContainer.SetActive(true);
    }

    public void OnEscClicked()
    {
        if ((slidersContainer != null && slidersContainer.activeSelf) ||
            (controllerContainer != null && controllerContainer.activeSelf))
        {
            if (slidersContainer != null) slidersContainer.SetActive(false);
            if (controllerContainer != null) controllerContainer.SetActive(false);
            if (mainSettingsGroup != null) mainSettingsGroup.SetActive(true);
        }
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

    // Escape handling removed from here — PauseManager is now the single authority (see below)
}