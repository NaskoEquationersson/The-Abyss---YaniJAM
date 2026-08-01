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
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    

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
        }

        Vector3 direction = new Vector3(inputVector.x, inputVector.z, inputVector.y);
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        // Ground check happens on the physics clock, consistent with the physics state
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, checkRadius, groundLayer);

        if (jumpRequested)
        {
            jumpRequested = false; // consumed immediately — can't fire twice even if isGrounded is stale for a frame

            if (isGrounded)
            {
                // Zero existing vertical velocity first so old upward velocity can't stack with the new impulse
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
