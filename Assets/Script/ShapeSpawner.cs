using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject spherePrefab;

    public TextAsset colorConfigJson; // Assign this in the Inspector

    public Transform playerCamera;
    public float minDistance = 5.0f;
    public float maxDistance = 20.0f;

    public float spawnInterval = 5.0f; // Time interval between spawns
    private float nextSpawnTime;

    private ColorConfigList colorConfig; // Store the loaded configuration

    private void Start()
    {
        LoadColorConfig();
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void LoadColorConfig()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Game_config");
        if (jsonFile != null)
        {
            colorConfig = JsonUtility.FromJson<ColorConfigList>(jsonFile.text);
            Debug.Log("Found the Json file");
        }
        else
        {
            Debug.LogError("Game_config.json not found in Resources folder.");
        }
    }

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

        Vector3 spawnPosition = playerCamera.position + playerCamera.forward * Random.Range(minDistance, maxDistance);
        GameObject newShape = Instantiate(shapePrefab, spawnPosition, Quaternion.identity);

        Color randomColor = new Color(Random.value, Random.value, Random.value);
        if (colors.Length > 0)
        {
            ColorConfig selectedColor = colors[Random.Range(0, colors.Length)];
            randomColor = new Color(selectedColor.r, selectedColor.g, selectedColor.b);
        }

        Renderer shapeRenderer = newShape.GetComponent<Renderer>();
        if (shapeRenderer != null)
        {
            shapeRenderer.material.color = randomColor;
        }
    }
}

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
