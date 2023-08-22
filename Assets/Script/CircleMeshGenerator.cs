using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CircleMeshGenerator : MonoBehaviour
{
    public int segments = 32;
    public float radius = 1.0f;
    
    private void Start()
    {
        GenerateCircle();
       
    }

    private void GenerateCircle()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.PI * 2.0f * (float)i / segments;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            vertices[i + 1] = new Vector3(x, y, 0);

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

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
