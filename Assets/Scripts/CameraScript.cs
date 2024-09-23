using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // The object the camera will follow
    public Transform target; 
    // Speed of rotation around the target using the mouse
    public float rotationSpeed = 8.0f; 
    // How far the camera is from the target
    public float distance = 10.0f; 
    // The smoothness of the camera's movement
    public float smoothTime = 0.1F;
    // Used for smooth damp function
    private Vector3 velocity = Vector3.zero; 
    // The current angle of the camera around the target
    private float currentAngleY; 

    void Start()
    {
        // Calculate the initial angle based on the camera's starting position
        Vector3 offset = transform.position - target.position;

        // Get the correct angle
        currentAngleY = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
    }

    void Update()
    {
        // Capture the horizontal mouse movement (left and right)
        float horizontalInput = Input.GetAxis("Mouse X");

        // Update the current angle of the camera around the Y axis
        currentAngleY += horizontalInput * rotationSpeed;

        // Create an offset from the target based on the current angle
        Vector3 offset = new Vector3(0, 5, -distance);
        Quaternion rotation = Quaternion.Euler(0, currentAngleY, 0);
        Vector3 targetPosition = target.position + rotation * offset;

        // Move the camera towards that new target position (smoothly)
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Make sure the camera is always looking at the target object
        transform.LookAt(target);
    }
}