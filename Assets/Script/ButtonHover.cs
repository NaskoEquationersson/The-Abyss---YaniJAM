using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    [SerializeField] private float hoverScale = 1.1f; // Ne kadar büyüsün?
    [SerializeField] private float speed = 10f; // Büyüme hızı

    private Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        // Yumuşak büyüme ve küçülme (Smooth scaling)
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    // Mouse üstüne gelince
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
    }

    // Mouse ayrılınca
    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }
}