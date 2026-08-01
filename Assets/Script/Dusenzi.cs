using UnityEngine;
using UnityEngine.InputSystem;

public class Dusenzi : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    [SerializeField] private Rigidbody rigidbody;
    private Vector3 velocity;
    public Vector3 offset;

    [Header("Mouse Look")]
    public float mouseSensitivity = 3f;
    public float minPitch = -20f;
    public float maxPitch = 60f;

    private float yaw;
    private float pitch = 20f;
    private bool shiftLockActive;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            shiftLockActive = !shiftLockActive;
            Cursor.lockState = shiftLockActive ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !shiftLockActive;
        }

        bool rotating = shiftLockActive || (Mouse.current != null && Mouse.current.leftButton.isPressed);

        if (rotating && Mouse.current != null)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();
            yaw += delta.x * mouseSensitivity * Time.deltaTime;
            pitch -= delta.y * mouseSensitivity * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }

    void LateUpdate()
    {
        if (target != null && rigidbody != null)
        {
            velocity = rigidbody.linearVelocity;

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 rotatedOffset = rotation * offset;

            Vector3 desiredPosition = target.position + rotatedOffset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target.position);
        }
    }
}