using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject rectanglePrefab;
    public GameObject pyramidPrefab;
    public float spawnInterval = 2.0f; // Time interval between spawning shapes
    public float destroyTime = 5.0f;   // Time after which spawned shapes are destroyed

    public string cameraTag = "camera"; // Tag of the camera object
    public float minSpawnDistance = 5.0f; // Minimum distance from camera
    public float maxSpawnDistance = 10.0f; // Maximum distance from camera
    public float minDistanceBetweenShapes = 2.0f; // Minimum distance between spawned shapes

    private float timeSinceLastSpawn = 0.0f;
    private GameObject cameraObject;
    private int shapeTypeIndex = 0;
    private Vector3 lastSpawnPosition;

    private void Start()
    {
        cameraObject = GameObject.FindGameObjectWithTag(cameraTag);
        GenerateNextShape(); // Generate the first shape immediately on start
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            GenerateNextShape();
            timeSinceLastSpawn = 0.0f;
        }
    }

    private void GenerateNextShape()
    {
        // Calculate a random distance between min and max spawn distances
        float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        // Calculate the spawn position in front of the camera
        Vector3 cameraPosition = cameraObject.transform.position;
        Vector3 cameraForward = cameraObject.transform.forward;
        Vector3 spawnPosition = cameraPosition + cameraForward * spawnDistance;

        // Ensure minimum distance between shapes
        if (lastSpawnPosition != Vector3.zero && Vector3.Distance(spawnPosition, lastSpawnPosition) < minDistanceBetweenShapes)
        {
            spawnPosition += cameraForward * minDistanceBetweenShapes;
        }

        GameObject shapePrefab = null;

        switch (shapeTypeIndex)
        {
            case 0:
                shapePrefab = circlePrefab;
                break;
            case 1:
                shapePrefab = rectanglePrefab;
                break;
            case 2:
                shapePrefab = pyramidPrefab;
                break;
        }

        if (shapePrefab != null)
        {
            GameObject newShape = Instantiate(shapePrefab, spawnPosition, Quaternion.identity);
            Destroy(newShape, destroyTime);

            shapeTypeIndex = (shapeTypeIndex + 1) % 3; // Cycle through 0, 1, 2
            lastSpawnPosition = spawnPosition; // Update last spawn position
        }
    }
}
