using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the target (ship) to follow
    public Vector3 offset; // Offset from the target's position
    public float speed; // Movement speed of the camera
    public float mouseSensitivity; // Sensitivity of mouse look

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void LateUpdate()
    {
        if (target != null)
        {
            // Handle user input for camera rotation
            xRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
            yRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Clamp vertical rotation to avoid flipping the camera upside down
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);

            // Update camera rotation based on input
            Quaternion cameraRotation = Quaternion.Euler(yRotation, xRotation, 0f);
            transform.rotation = cameraRotation;

            // Apply target following with offset
            transform.position = target.position + target.TransformDirection(offset);

            // Handle user input for ship rotation
            float rotationInput = Input.GetAxis("Horizontal");
            target.Rotate(0f, rotationInput * speed * Time.deltaTime, 0f);
        }
    }
}
