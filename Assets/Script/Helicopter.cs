using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public int moneyPerHostage = 10;

    void OnTriggerEnter(Collider other)
    {
        Hostage hostage = other.GetComponent<Hostage>();

        if (hostage != null && hostage.IsFollowing())
        {
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.AddMoney(moneyPerHostage);
            }

            Destroy(other.gameObject); // hostage "delivered" - remove from scene
        }
    }
}