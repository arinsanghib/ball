using UnityEditor;

using UnityEngine;

// Require mesh filter and renderer components
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class CircleMeshGenerator : MonoBehaviour

{

    // Number of segments to generate circle with
    public int segments = 32;

    // Radius of the circle
    public float radius = 1.0f;

    private void Start()

    {

        // Generate the circle mesh
        GenerateCircle();

    }

    private void GenerateCircle()

    {

        // Get mesh filter component
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        // Create new mesh
        Mesh mesh = new Mesh();

        // Assign to mesh filter
        meshFilter.mesh = mesh;

        // Vertex array
        Vector3[] vertices = new Vector3[segments + 1];

        // Triangle array
        int[] triangles = new int[segments * 3];

        // Center vertex
        vertices[0] = Vector3.zero;

        // Calculate positions of vertices around a circle
        for (int i = 0; i < segments; i++)

        {

            float angle = Mathf.PI * 2.0f * (float)i / segments;

            float x = Mathf.Cos(angle) * radius;

            float y = Mathf.Sin(angle) * radius;

            vertices[i + 1] = new Vector3(x, y, 0);

            // Connect vertices into triangles
            if (i < segments - 1)

            {

                triangles[i * 3] = 0;

                triangles[i * 3 + 1] = i + 1;

                triangles[i * 3 + 2] = i + 2;

            }

            else

            {

                triangles[i * 3] = 0;

                triangles[i * 3 + 1] = i + 1;

                triangles[i * 3 + 2] = 1;

            }

        }

        // Assign vertices and triangles
        mesh.vertices = vertices;

        mesh.triangles = triangles;

        // Recalculate normals for lighting
        mesh.RecalculateNormals();

    }

}