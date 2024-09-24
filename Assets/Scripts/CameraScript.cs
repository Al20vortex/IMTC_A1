using UnityEngine;

/*
    This script is responsible for camera following behaviour, and handles mouse input to pan the camera. 
    The camera follows the player square automatically, and smoothly. 
    The camera direction also changes smoothly.
*/
public class CameraScript : MonoBehaviour
{
    // The object the camera will follow (In this case the player)
    public Transform target; 
    // Rotation sensitivity, higher is more sensitive.
    public float rotationSpeed = 8.0f; 
    // How far the camera should be from the target
    public float distance = 10.0f; 
    // The smoothness of the camera's movement, lower values correlate to a more responsive and less smooth camera.
    public float smoothTime = 0.1F;
    private Vector3 velocity = Vector3.zero; 
    // The current angle of the camera around the target
    private float currentAngleY; 

    void Start() // only runs at the Start
    {
        // Calculate the initial angle based on the camera's starting position
        // This initial angle is crucial because all calculations afterwards are adjustments to this angle
        Vector3 offset = transform.position - target.position;
        currentAngleY = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
    }

    void Update() // once every game tick
    {
        // Capture the horizontal mouse movement (left and right) so that we can later make changes to the camera direction.
        float horizontalInput = Input.GetAxis("Mouse X");

        // Update the current angle of the camera around the Y axis
        currentAngleY += horizontalInput * rotationSpeed;

        // Create an offset from the target based on the current angle
        // The offset vector defines the camera's relative position compared to the target. 
        // 5 units above, and -distance away on the z axis. It could have been on the x axis instead, it doesn't matter too much.
        Vector3 offset = new Vector3(0, 5, -distance);

        // Euler function is used to get a rotation that 
        //  rotates z degrees around the z axis, 
        //  x degrees around the x axis, and y degrees around the y axis, in that respective order.
        Quaternion rotation = Quaternion.Euler(0, currentAngleY, 0);
        
        // We use the new rotation vector to multiply it by the offset vector to rotate it by currentAngleY degrees on the Y axis.
        // This places the camera at the correct position relative to the target object after the rotation cause by mouse movement.
        Vector3 targetPosition = target.position + rotation * offset;

        // Move the camera towards that new target position (smoothly)
        // The way it works is by slowly reducing the distance between the current position and the target position over time (we use smoothTime)
        // This makes the transition super smooth.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Make sure the camera is always looking at the target object, not only following it.
        transform.LookAt(target);
    }
}