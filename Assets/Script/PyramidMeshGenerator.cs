using UnityEngine;

// Require mesh filter and renderer 
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class PyramidMeshGenerator : MonoBehaviour
{

    // Size of the pyramid
    public float size = 1.0f;

    private void Start()
    {
        // Generate pyramid mesh
        GeneratePyramid();
    }

    private void GeneratePyramid()
    {
        // Get mesh filter
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        // Create new mesh
        Mesh mesh = new Mesh();

        // Assign to filter
        meshFilter.mesh = mesh;

        // Vertex array
        Vector3[] vertices = new Vector3[5];

        // Triangle array
        int[] triangles = new int[18];

        // Define vertex positions
        vertices[0] = new Vector3(0, size * 0.5f, 0);
        vertices[1] = new Vector3(-size * 0.5f, -size * 0.5f, size * 0.5f);
        vertices[2] = new Vector3(size * 0.5f, -size * 0.5f, size * 0.5f);
        vertices[3] = new Vector3(size * 0.5f, -size * 0.5f, -size * 0.5f);
        vertices[4] = new Vector3(-size * 0.5f, -size * 0.5f, -size * 0.5f);

        // Define triangles
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        //...

        // Assign vertex and triangle arrays
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals
        mesh.RecalculateNormals();
    }

}