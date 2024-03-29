using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder : MonoBehaviour
{
    private readonly List<Vector3> verticies = new List<Vector3>();
    private readonly List<Vector3> normals = new List<Vector3>();
    private readonly List<Vector2> uv = new List<Vector2>();
    private readonly List<int> triangles = new List<int>();
    public Matrix4x4 VertexMatrix = Matrix4x4.identity;
    public Matrix4x4 TextureMatrix = Matrix4x4.identity;

    public int AddVertex(Vector3 position, Vector3 normal, Vector2 uv)
    {
        int index = verticies.Count;
        verticies.Add(VertexMatrix.MultiplyPoint(position));
        normals.Add(VertexMatrix.MultiplyVector(normal));
        this.uv.Add(TextureMatrix.MultiplyPoint(uv));
        return index;
    }

    public void AddQuad(int bottomLeft, int topLeft, int topRight, int bottomRight)
    {
        // First triangle
        triangles.Add(bottomLeft);
        triangles.Add(topLeft);
        triangles.Add(topRight);
            
        // Second triangle
        triangles.Add(bottomLeft);
        triangles.Add(topRight);
        triangles.Add(bottomRight);
    }

    public void AddTriangle(int bottomRight, int bottomLeft, int topCorner)
    {
        triangles.Add(bottomRight);
        triangles.Add(bottomLeft);
        triangles.Add(topCorner);
    }

    public void Build(Mesh mesh)
    {
        mesh.Clear();
        mesh.SetVertices(verticies);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uv);
        mesh.SetIndices(triangles, MeshTopology.Triangles, 0);
        mesh.MarkModified();
        mesh.RecalculateTangents();
        verticies.Clear();
        triangles.Clear();
        normals.Clear();
        uv.Clear();
    }
}
