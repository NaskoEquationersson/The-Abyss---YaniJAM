using UnityEngine;
using UnityEngine.InputSystem;

public class Axe : MonoBehaviour
{
    public bool isUnlocked = false; // ShopManager can enable this GameObject to "unlock" it, or set this directly
    public float extinguishRange = 2f;
    public float extinguishTime = 2f; // how long you must hold near fire
    private float holdTimer = 0f;

    private FireHazard nearbyFire;

    void Update()
    {
        if (!isUnlocked) return;

        if (nearbyFire != null && Keyboard.current != null && Keyboard.current.fKey.isPressed)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= extinguishTime)
            {
                Destroy(nearbyFire.gameObject); // simplest jam version: fire just disappears once extinguished
                holdTimer = 0f;
            }
        }
        else
        {
            holdTimer = 0f; // reset if key released or fire out of range
        }
    }

    void OnTriggerEnter(Collider other)
    {
        FireHazard fire = other.GetComponent<FireHazard>();
        if (fire != null) nearbyFire = fire;
    }

    void OnTriggerExit(Collider other)
    {
        FireHazard fire = other.GetComponent<FireHazard>();
        if (fire == nearbyFire) nearbyFire = null;
    }
}