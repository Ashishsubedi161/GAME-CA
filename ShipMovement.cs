using UnityEngine;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    public float speed = 10.0f; // Forward movement speed
    public float turnSpeed = 5.0f; // Rotation speed
    public GameObject cannonballPrefab; // Reference to the cannonball prefab
    public Transform cannonSpawnPoint; // Transform representing the position where the cannonball will be spawned
    public AudioClip cannonSound; // Sound to play when firing the cannon
    public AudioClip reloadSound; // Sound to play when reloading
    public GameObject cannonFireEffectPrefab; // Particle system prefab for cannon firing effect
    public float reloadTime = 1.0f; // Time between shots
    private float lastShootTime = 0.0f; // Time when the last shot was fired

    private AudioSource audioSource; // Reference to the AudioSource component

    private Rigidbody rb;
        public Text gameOverText; 
    public GameObject textObject;

    void Start()
    {
 
{
    Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen
    Cursor.visible = false; // Hide cursor
}
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("ShipMovement script requires a Rigidbody component on the same GameObject.");
        }

        // Get the AudioSource component attached to this GameObject or any other GameObject in the scene
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = FindObjectOfType<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("No AudioSource component found in the scene.");
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Z))
        {
            if (Time.time - lastShootTime >= reloadTime)
            {
                ShootCannonball();
            }
        }
    }

    void FixedUpdate()
{
    // Handle forward and backward movement
    float movementInput = Input.GetAxis("Vertical"); // Get input for forward and backward movement
    float thrust = Mathf.Max(0, Mathf.Abs(movementInput)); // Use absolute value to handle both positive (forward) and negative (backward) movement
    rb.AddForce(transform.forward * movementInput * speed * Time.deltaTime * 1000); // Adjusted force multiplier

    // Handle rotation
    float turn = Input.GetAxis("Horizontal"); // Use "Horizontal" axis for left/right rotation
    Quaternion turnRotation = Quaternion.Euler(0f, turn * turnSpeed * Time.deltaTime, 0f);
    rb.MoveRotation(rb.rotation * turnRotation);
}

void ShootCannonball()
{
    if (cannonballPrefab != null && cannonSpawnPoint != null)
    {
        // Calculate the direction for shooting (negative x-axis of the spawn point)
        Vector3 shootingDirection = -cannonSpawnPoint.right; // Assuming the spawn point's negative x-axis is pointing towards the shooting direction

        RaycastHit hit;
        if (Physics.Raycast(cannonSpawnPoint.position, shootingDirection, out hit))
        {
            // Calculate force direction towards the hit point
            Vector3 forceDirection = (hit.point - cannonSpawnPoint.position).normalized;

            // Instantiate cannonball at the spawn point
            GameObject cannonballObject = Instantiate(cannonballPrefab, cannonSpawnPoint.position, Quaternion.identity);
            Rigidbody cannonballRb = cannonballObject.GetComponent<Rigidbody>();

            // Check if Rigidbody component is present
            if (cannonballRb != null)
            {
                // Add force to the cannonball in the direction of the hit point
                cannonballRb.AddForce(forceDirection * 5000f); // Adjust force as needed
            }
            else
            {
                Debug.LogError("Cannonball prefab does not have a Rigidbody component attached.");
            }

            // Destroy the cannonball object after a delay (e.g., 3 seconds)
            Destroy(cannonballObject, 3f);
        }
        else
        {
            // Instantiate cannonball at the spawn point with default forward direction
            GameObject cannonballObject = Instantiate(cannonballPrefab, cannonSpawnPoint.position, Quaternion.identity);
            Rigidbody cannonballRb = cannonballObject.GetComponent<Rigidbody>();

            // Check if Rigidbody component is present
            if (cannonballRb != null)
            {
                // Add force to the cannonball in the default forward direction
                cannonballRb.AddForce(shootingDirection * 5000f); // Adjust force as needed
            }
            if (cannonFireEffectPrefab != null)
{
    GameObject cannonFireEffect = Instantiate(cannonFireEffectPrefab, cannonSpawnPoint.position, Quaternion.identity);
    Destroy(cannonFireEffect, 1.0f); // Destroy the particle system after 1 second (adjust the time as needed)
}
            else
            {
                Debug.LogError("Cannonball prefab does not have a Rigidbody component attached.");
            }

            // Destroy the cannonball object after a delay (e.g., 3 seconds)
            Destroy(cannonballObject, 3f);
        }

        // Play the cannon sound if it's assigned and the AudioSource component is available
        if (audioSource != null && cannonSound != null)
        {
            audioSource.PlayOneShot(cannonSound);
        }
        

        // Play the reload sound if it's assigned and the AudioSource component is available
        if (audioSource != null && reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        // Record the time when the shot was fired
        lastShootTime = Time.time;
    }
    else
    {
        Debug.LogError("Cannonball prefab or cannon spawn point is not assigned to the ShipMovement script.");
    }
}

void OnCollisionEnter(Collision collision){
            if (collision.gameObject.CompareTag("Enemy"))
        {
            // Activate the game over text
            textObject.SetActive(true);
            Time.timeScale = 0f;
           
        }
}


}
