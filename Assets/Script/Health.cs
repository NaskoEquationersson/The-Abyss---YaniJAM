using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI (optional, leave empty for hostages without a visible bar)")]
    public Slider healthBarSlider;
    [Header("Regeneration")]
    public bool canRegenerate = true;
    public float regenDelay = 3f;      // seconds after last damage before regen starts
    public float regenRate = 5f;       // health per second once regen kicks in

    private float lastDamageTime = -999f;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        lastDamageTime = Time.time; // regen waits this long after the most recent hit
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        if (CompareTag("Player") && DeathScreenManager.Instance != null)
        {
            DeathScreenManager.Instance.ShowDeathScreen();
        }
        else
        {
            gameObject.SetActive(false); // hostages still just disappear on death
        }
    }
    void Update()
    {
        if (canRegenerate && currentHealth < maxHealth && currentHealth > 0)
        {
            if (Time.time - lastDamageTime >= regenDelay)
            {
                currentHealth += Mathf.RoundToInt(regenRate * Time.deltaTime);
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                UpdateUI();
            }
        }
    }
}