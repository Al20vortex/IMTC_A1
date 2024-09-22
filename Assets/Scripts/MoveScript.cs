using UnityEngine;

public class MoveScript : MonoBehaviour
{

    // Set the speed at which the cube moves in its forward direction 
    public float speed = 1f;          
    // How many degrees the Cube Rotates per press  
    public float rotationSpeed = 10f;
    // Prefab for the projectile that the cube ejects.
    public GameObject projectilePrefab;
    // Projectile speed
    public float projectileSpeed = 50f;

    void Update()
    {
        // Handle input
        keyHandler();
    }

    void keyHandler()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Fire the projectile if space pressed.
            fireProjectile();
        }
        if (Input.GetKey(KeyCode.W)) {
            // Move the cube forward in its local forward direction
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) {
            // Move backwards
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) {
            // Rotate to the left
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            // Rotate to the right
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

    }

    void fireProjectile()
    {
        // Instantiate the projectile at the cube's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Add a Rigidbody component if not already present
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = projectile.AddComponent<Rigidbody>();
        }

        // Disable gravity on the projectile (optional)
        rb.useGravity = false;

        // Set the projectile to move forward
        rb.linearVelocity = transform.forward * projectileSpeed;
    }
}
