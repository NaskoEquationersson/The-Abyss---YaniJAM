using UnityEngine;
using UnityEngine.InputSystem;

public class MinimapController : MonoBehaviour
{
    [Header("References")]
    public GameObject minimapCanvas; // the UI panel/RawImage holding the minimap
    public Transform player;
    public Camera minimapCamera; // a second camera, positioned above the scene looking straight down

    [Header("Settings")]
    public float heightAboveMap = 50f;
    public bool startVisible = false;

    void Start()
    {
        if (minimapCanvas != null) minimapCanvas.SetActive(startVisible);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
        {
            if (minimapCanvas != null) minimapCanvas.SetActive(!minimapCanvas.activeSelf);
        }
    }

    void LateUpdate()
    {
        // Keep minimap camera centered above the player at all times
        if (player != null && minimapCamera != null)
        {
            Vector3 pos = player.position;
            pos.y += heightAboveMap;
            minimapCamera.transform.position = pos;
            // Looking straight down; rotation stays fixed (world-north up) - set once in Inspector as (90, 0, 0)
        }
    }
}