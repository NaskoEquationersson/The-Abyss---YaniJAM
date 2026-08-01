using UnityEngine;

public class Dusenzi : MonoBehaviour
{

    // Camera mainCamera = Camera.main;
    public Transform target;
    public float smoothSpeed = 0.125f;
    // private Vector3 velocity = Vector3.zero;
    [SerializeField] private Rigidbody rigidbody;
    private Vector3 velocity;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target != null && rigidbody != null)
        {
            velocity = rigidbody.linearVelocity;
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
