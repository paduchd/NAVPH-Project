using UnityEngine;

public class FloatingBox : MonoBehaviour
{
    [SerializeField] private float floatHeight = 0.05f; // Adjust the height of the floating motion
    [SerializeField] private float floatSpeed = 0.5f; // Adjust the speed of the floating motion
    [SerializeField] private float rotationAmount = 3.0f; // Adjust the rotation amount

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the box
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the vertical offset based on a sine wave
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Update the box's position to create the floating effect
        transform.position = initialPosition + new Vector3(0, yOffset, 0);

        // Calculate the rotation angle based on the sine wave
        float angle = Mathf.Sin(Time.time * floatSpeed) * rotationAmount;

        // Apply rotation around the forward axis (change to other axes for different effects)
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}