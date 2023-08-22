using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RectangleMeshGenerator : MonoBehaviour
{
    public float width = 1.0f;
    public float height = 1.0f;
   

    private void Start()
    {
  
        GenerateRectangle();
    }

    private void GenerateRectangle()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(-width * 0.5f, -height * 0.5f, 0);
        vertices[1] = new Vector3(width * 0.5f, -height * 0.5f, 0);
        vertices[2] = new Vector3(-width * 0.5f, height * 0.5f, 0);
        vertices[3] = new Vector3(width * 0.5f, height * 0.5f, 0);

        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
