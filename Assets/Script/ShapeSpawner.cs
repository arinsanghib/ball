using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{

    // Prefabs to spawn
    public GameObject cubePrefab;
    public GameObject spherePrefab;

    // Json color config data
    public TextAsset colorConfigJson;

    // Player camera transform
    public Transform playerCamera;

    // Min and max spawn distances
    public float minDistance = 5.0f;
    public float maxDistance = 20.0f;

    // Time between spawns
    public float spawnInterval = 5.0f;

    // Next spawn time
    private float nextSpawnTime;

    // Loaded color config
    private ColorConfigList colorConfig;

    private void Start()
    {
        // Load color config data
        LoadColorConfig();

        // Initialize next spawn time
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void LoadColorConfig()
    {
        // Load json config file
        TextAsset jsonFile = Resources.Load<TextAsset>("Game_config");

        if (jsonFile != null)
        {
            // Parse json into config object
            colorConfig = JsonUtility.FromJson<ColorConfigList>(jsonFile.text);
            Debug.Log("Found the Json file");
        }
        else
        {
            Debug.LogError("Game_config.json not found in Resources folder.");
        }
    }

    // Spawn shapes at regular intervals
    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRandomShape();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnRandomShape()
    {
        // Pick random shape
        GameObject shapePrefab;
        ColorConfig[] colors;

        if (Random.Range(0, 2) == 0)
        {
            shapePrefab = cubePrefab;
            colors = colorConfig.cubeColors;
        }
        else
        {
            shapePrefab = spherePrefab;
            colors = colorConfig.sphereColors;
        }

        // Spawn at random distance
        Vector3 spawnPosition = playerCamera.position + playerCamera.forward * Random.Range(minDistance, maxDistance);

        // Instantiate shape
        GameObject newShape = Instantiate(shapePrefab, spawnPosition, Quaternion.identity);

        // Pick random color
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        if (colors.Length > 0)
        {
            // Or pick from config
            ColorConfig selectedColor = colors[Random.Range(0, colors.Length)];
            randomColor = new Color(selectedColor.r, selectedColor.g, selectedColor.b);
        }

        // Set shape color
        Renderer shapeRenderer = newShape.GetComponent<Renderer>();
        if (shapeRenderer != null)
        {
            shapeRenderer.material.color = randomColor;
        }
    }

}

// Color config classes
[System.Serializable]
public class ColorConfig
{
    public float r;
    public float g;
    public float b;
}

[System.Serializable]
public class ColorConfigList
{
    public ColorConfig[] sphereColors;
    public ColorConfig[] cubeColors;
    public ColorConfig ballColor;
}