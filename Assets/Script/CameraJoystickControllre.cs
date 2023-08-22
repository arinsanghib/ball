using UnityEngine;

public class CameraJoystickControllre : MonoBehaviour
{
    public Joystick cameraJoystick;
    // Assign this in the Inspector
    public GameObject projectilePrefab;
    // Prefab to spawn and launch
    public float launchForce = 10.0f;
    // Force to launch the projectile
    public float rotationSpeed = 50.0f;
    // Speed of camera rotation
    public float moveSpeed = 2.0f;
    // Speed of camera movement
    public float spawnInterval = 2.0f;
    // Time interval between spawning projectiles
    public float joystickThreshold = 0.5f;
    // Minimum joystick input magnitude to launch
    public float minYLimit = -5.0f;
    // Minimum Y-axis limit
    public float maxYLimit = 5.0f;
    // Maximum Y-axis limit

    private Transform cameraTransform;
    // Reference to the camera's transform
    private float timeSinceLastSpawn = 0.0f;
    // Time since the last projectile was spawned

    private void Start()
    {
        // Get the camera's and child's transforms
        cameraTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        RotateCamera();
        MoveCamera();

        timeSinceLastSpawn += Time.deltaTime;

        // Check only the vertical component of the joystick input for launching
        float joystickInputY = cameraJoystick.Vertical;

        if (joystickInputY > joystickThreshold && timeSinceLastSpawn >= spawnInterval)
        {
            SpawnAndLaunch();
            timeSinceLastSpawn = 0.0f;
        }
    }

    private void RotateCamera()
    {
        float joystickInputX = cameraJoystick.Horizontal;

        // Calculate the rotation amounts based on joystick inputs
        float rotationAmountX = -joystickInputX * rotationSpeed * Time.deltaTime;

        // Rotate the camera around its local X-axis (up and down rotation)
        cameraTransform.Rotate(Vector3.up, rotationAmountX);
    }

    private void MoveCamera()
    {
        float joystickInputY = cameraJoystick.Vertical;

        // Calculate the movement amount based on joystick input
        float movementAmountY = joystickInputY * moveSpeed * Time.deltaTime;

        // Calculate the new Y position within limits
        float newYPosition = Mathf.Clamp(cameraTransform.position.y + movementAmountY, minYLimit, maxYLimit);

        // Move the camera along its local Y-axis within limits
        cameraTransform.position = new Vector3(cameraTransform.position.x, newYPosition, cameraTransform.position.z);
    }

    private void SpawnAndLaunch()
    {
        // Calculate the spawn position as the middle of the camera
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward;

        // Spawn the projectile at the calculated spawn position
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Calculate the launch direction based on the camera's forward direction
        Vector3 launchDirection = cameraTransform.forward;

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();

        // Apply launch force to the projectile's Rigidbody
        projectileRigidbody.velocity = launchDirection * launchForce;
    }
}
