using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class MehmetEken : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public Rigidbody rb;

    private bool isGrounded;
    private bool jumpRequested;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpCooldown = 0.2f; // seconds
    private float lastJumpTime = -999f;

    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Camera Reference")]
    public Transform cameraTransform;
    private Vector3 moveDirection; 

    public Vector3 LastMoveDirection { get; private set; } = Vector3.forward;

    [Header("Sprint Settings")]
    public float sprintMultiplier = 1.6f;
    private bool isSprinting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
        Vector3 inputVector = Vector3.zero;

        if (Keyboard.current != null)
        {
            // WASD veya Ok Tuşlarını kontrol eder
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) inputVector.y = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) inputVector.y = -1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) inputVector.x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) inputVector.x = 1f;
            if (Keyboard.current.spaceKey.wasPressedThisFrame) 
            {
                jumpRequested = true;
            }   
            if (Keyboard.current.leftShiftKey.isPressed)
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }
        }

        Vector3 rawDirection = new Vector3(inputVector.x, 0, inputVector.y);
        if (rawDirection.sqrMagnitude > 1f)
        {
            rawDirection.Normalize();
        }
        // rb.MovePosition(rb.position + rawDirection * speed * Time.fixedDeltaTime);
        Quaternion cameraYawRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        moveDirection = cameraYawRotation * rawDirection; // store in a field so FixedUpdate can use it

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            LastMoveDirection = moveDirection.normalized; // only update when actually moving, so it holds last-faced direction when idle
        }

    }


    void FixedUpdate()
    {
        // Ground check happens on the physics clock, consistent with the physics state
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, checkRadius, groundLayer);

        if (jumpRequested)
        {
            jumpRequested = false; // consumed immediately — can't fire twice even if isGrounded is stale for a frame

            bool cooldownPassed = Time.time - lastJumpTime > jumpCooldown;

            if (isGrounded && cooldownPassed)
            {
                // Zero existing vertical velocity first so old upward velocity can't stack with the new impulse
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                lastJumpTime = Time.time;
            }
        }

        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);
    }
}
