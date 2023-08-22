using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PyramidMeshGenerator : MonoBehaviour
{
    public float size = 1.0f;


    private void Start()
    {

        GeneratePyramid();
    }

    private void GeneratePyramid()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[5];
        int[] triangles = new int[18];

        vertices[0] = new Vector3(0, size * 0.5f, 0);
        vertices[1] = new Vector3(-size * 0.5f, -size * 0.5f, size * 0.5f);
        vertices[2] = new Vector3(size * 0.5f, -size * 0.5f, size * 0.5f);
        vertices[3] = new Vector3(size * 0.5f, -size * 0.5f, -size * 0.5f);
        vertices[4] = new Vector3(-size * 0.5f, -size * 0.5f, -size * 0.5f);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        triangles[6] = 0;
        triangles[7] = 3;
        triangles[8] = 4;

        triangles[9] = 0;
        triangles[10] = 4;
        triangles[11] = 1;

        triangles[12] = 1;
        triangles[13] = 3;
        triangles[14] = 2;

        triangles[15] = 1;
        triangles[16] = 4;
        triangles[17] = 3;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
