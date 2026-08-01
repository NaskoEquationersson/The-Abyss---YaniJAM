using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

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
        // 2. Klavyeden anlık yön girdilerini okuyun
        Vector3 inputVector = Vector3.zero;

        if (Keyboard.current != null)
        {
            // WASD veya Ok Tuşlarını kontrol eder
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) inputVector.y = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) inputVector.y = -1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) inputVector.x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) inputVector.x = 1f;

            if (Keyboard.current.spaceKey.isPressed) inputVector.z = 1f;
            if (Keyboard.current.leftShiftKey.isPressed) inputVector.z = -1f;
        }

        // 3. Girdileri 3 boyutlu hareket vektörüne dönüştürün
        Vector3 direction = new Vector3(inputVector.x, inputVector.z, inputVector.y);

        // 4. Objeyi hareket ettirin
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
    
}
