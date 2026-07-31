using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MehmetEken : MonoBehaviour
{

    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
