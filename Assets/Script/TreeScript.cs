using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject normalVisual;
    public GameObject burningVisual; // separate model/sprite, or same object with different material

    [Header("Fire")]
    public FireHazard fireHazardPrefabOrComponent; // the FireHazard component to enable once burning

    private bool isBurning = false;

    void Start()
    {
        SetVisualState();
    }

    public bool IsBurning()
    {
        return isBurning;
    }

    public void SetOnFire()
    {
        if (isBurning) return;

        isBurning = true;
        SetVisualState();

        if (fireHazardPrefabOrComponent != null)
        {
            fireHazardPrefabOrComponent.enabled = true;
            fireHazardPrefabOrComponent.isBurning = true;
        }
    }

    void SetVisualState()
    {
        if (normalVisual != null) normalVisual.SetActive(!isBurning);
        if (burningVisual != null) burningVisual.SetActive(isBurning);
    }
}