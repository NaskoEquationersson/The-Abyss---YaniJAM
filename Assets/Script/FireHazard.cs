using UnityEngine;

public class FireHazard : MonoBehaviour
{
    public int damagePerTick = 5;
    public float tickInterval = 1f;
    private float tickTimer = 0f;

    void OnTriggerStay(Collider other)
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;

            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damagePerTick);
            }
        }
    }
}