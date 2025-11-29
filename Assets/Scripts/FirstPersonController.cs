using UnityEngine;

// This script controls the movement and camera look for a first-person player.
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("The speed at which the player moves forward and backward.")]
    public float moveSpeed = 5.0f;
    [Tooltip("The height of the jump.")]
    public float jumpForce = 5.0f;

    [Header("Look Settings")]
    [Tooltip("The sensitivity of the mouse look.")]
    public float mouseSensitivity = 2.0f;
    [Tooltip("The minimum vertical angle the player can look.")]
    public float minLookAngle = -90f;
    [Tooltip("The maximum vertical angle the player can look.")]
    public float maxLookAngle = 90f;

    // Private variables for components and state
    private Rigidbody rb;
    private Transform cameraTransform;
    private float rotationX = 0f;
    private bool isGrounded;

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject (the Player Capsule)
        rb = GetComponent<Rigidbody>();
        
        // Find the child camera object's transform
        cameraTransform = GetComponentInChildren<Camera>()?.transform;

        // Check if the camera was found, otherwise log an error
        if (cameraTransform == null)
        {
            Debug.LogError("FirstPersonController requires a Camera child object to control the look direction.");
        }

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Movement Input ---
        // Get input values for movement
        float x = Input.GetAxis("Horizontal"); // A/D keys
        float z = Input.GetAxis("Vertical");   // W/S keys

        // Calculate the direction of movement relative to the player's forward direction
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        
        // Apply the movement velocity. We ignore the Y component (vertical) to preserve gravity/jumping.
        // We set the velocity directly for simple, non-accelerated movement.
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        
        // --- Jumping ---
        // Check if the jump button is pressed and the player is on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent double jumping
        }

        // --- Camera Look (Rotation) ---
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 1. Horizontal Rotation (Turning the Player Body)
        // Rotate the entire player object around the Y-axis
        transform.Rotate(Vector3.up * mouseX);

        // 2. Vertical Rotation (Tilting the Camera Up/Down)
        // Accumulate vertical rotation, making sure it stays within min/max angles
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minLookAngle, maxLookAngle);

        // Apply the vertical rotation to the camera only
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }

    // --- Ground Check ---
    // Use collision detection to determine if the player is touching the ground.
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is below the player (the floor)
        // We only mark as grounded if the contact point is roughly beneath the player
        if (Vector3.Angle(collision.contacts[0].normal, Vector3.up) < 45)
        {
            isGrounded = true;
        }
    }

    // Reset grounded state when leaving a collision surface
    private void OnCollisionExit(Collision collision)
    {
        // A slight delay or continuous check might be better for complex scenarios, 
        // but for a simple plane, exiting is usually fine.
        // isGrounded = false; // Keep it true until the next jump for simplicity here
    }
}