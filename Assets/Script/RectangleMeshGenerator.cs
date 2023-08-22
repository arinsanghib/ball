using UnityEngine;

// Require mesh filter and renderer
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RectangleMeshGenerator : MonoBehaviour
{
    // Width of rectangle
    public float width = 1.0f;

    // Height of rectangle
    public float height = 1.0f;

    private void Start()
    {
        // Generate rectangle mesh
        GenerateRectangle();
    }

    private void GenerateRectangle()
    {
        // Get mesh filter
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        // Create new mesh
        Mesh mesh = new Mesh();

        // Assign to filter
        meshFilter.mesh = mesh;

        // Vertex array
        Vector3[] vertices = new Vector3[4];

        // Triangle array
        int[] triangles = new int[6];

        // Define rectangle vertices
        vertices[0] = new Vector3(-width * 0.5f, -height * 0.5f, 0);
        vertices[1] = new Vector3(width * 0.5f, -height * 0.5f, 0);
        vertices[2] = new Vector3(-width * 0.5f, height * 0.5f, 0);
        vertices[3] = new Vector3(width * 0.5f, height * 0.5f, 0);

        // Define triangles
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;

        // Assign vertex and triangle arrays
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals
        mesh.RecalculateNormals();
    }

}