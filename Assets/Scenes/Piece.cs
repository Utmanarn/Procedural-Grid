using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.WSA;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(GridTile))]
public class Piece : MonoBehaviour
{
    private Mesh mesh;
    private GridTile tile;

    void Start()
    {
        tile = GetComponent<GridTile>();
        var meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh {name = "Tile"};
        meshFilter.sharedMesh = mesh;
    }
    
    /*private void OnDrawGizmos() {
        var tile = GetComponent<GridTile>();
        if (tile.GetProperty(GridTileProperty.Solid)) {
            Gizmos.color = Color.red;
        } else if (tile.GetProperty(GridTileProperty.Water))
        {
            Gizmos.color = Color.blue;
        }
        else if (tile.GetProperty(GridTileProperty.Road))
        {
            Gizmos.color = Color.black;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawCube(transform.position, new Vector3(1, 0.1f, 1));
    } */

    private void Update()
    {
        MeshBuilder builder = new MeshBuilder();

        if (tile.GetProperty(GridTileProperty.Solid))
        {
            SetRoad(builder);
        }
        else if (tile.GetProperty(GridTileProperty.Water))
        {
            SetWater(builder);
        }
        else
        {
            SetGrass(builder);
        }
    }

    private void SetWater(MeshBuilder builder)
    {
    
        builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.52f, 0.02f, 1f)) *
                                Matrix4x4.Scale(new Vector3(0.475f, 0.475f, 1f)); // Changed to fit with texture padding. This is to fix the problem with the texture borders bleeding over.
    
        int a = builder.AddVertex(
            new Vector3(0, -0.25f, 0),
            Vector3.up,
            new Vector2(0, 0));
        int b = builder.AddVertex(
            new Vector3(1, -0.25f, 0),
            Vector3.up, 
            new Vector2(1, 0));
        int c = builder.AddVertex(
            new Vector3(0, -0.25f, 1),
            Vector3.up, 
            new Vector2(0, 1));
        int d = builder.AddVertex(
            new Vector3(1, -0.25f, 1),
            Vector3.up, 
            new Vector2(1, 1));
        builder.AddQuad(a, c, d, b);
        
        // Set the wall textures to only use the top bit of the texture. Was advised to do this by Cecilia.
        builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.52f, 0.3f, 1.0f)) *
                                Matrix4x4.Scale(new Vector3(0.475f, 0.2f, 1.0f));
        // - START - Water Walls
        if (!tile.GetNeighbourProperty(0, GridTileProperty.Water)) // Right
        {
            builder.VertexMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(90, Vector3.up)) *
                                   Matrix4x4.Translate(new Vector3(-1, 0, 0));
            int e = builder.AddVertex(
                new Vector3(0, -0.25f, 0.85f),
                new Vector3(0, 0.25f, -0.25f),
                new Vector2(0, 0));
            int f = builder.AddVertex(
                new Vector3(1, -0.25f, 0.85f),
                new Vector3(0, 0.25f, -0.25f),
                new Vector2(1, 0));
            int g = builder.AddVertex(
                new Vector3(0, 0, 1),
                new Vector3(0, 0.25f, -0.25f),
                new Vector2(0, 1));
            int h = builder.AddVertex(
                new Vector3(1, 0, 1),
                new Vector3(0, 0.25f, -0.25f),
                new Vector2(1, 1));
            builder.AddQuad(e, g, h, f);
        }
        if (!tile.GetNeighbourProperty(2, GridTileProperty.Water)) // Top
        {
            builder.VertexMatrix = Matrix4x4.identity;
            int e = builder.AddVertex(
                new Vector3(0, -0.25f, 0.85f),
                new Vector3(0, 0.25f, -0.25f),
                new Vector2(0, 0));
            int f = builder.AddVertex(
                new Vector3(1, -0.25f, 0.85f),
                new Vector3(0, 0.25f, -0.25f), 
                new Vector2(1, 0));
            int g = builder.AddVertex(
                new Vector3(0, 0f, 1),
                new Vector3(0, 0.25f, -0.25f), 
                new Vector2(0, 1));
            int h = builder.AddVertex(
                new Vector3(1, 0f, 1),
                new Vector3(0, 0.25f, -0.25f), 
                new Vector2(1, 1));
            builder.AddQuad(e, g, h, f);
        }
        if (!tile.GetNeighbourProperty(4, GridTileProperty.Water)) // Left
        {
            builder.VertexMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(90, Vector3.down)) *
                                   Matrix4x4.Translate(new Vector3(0, 0, -1));
            int e = builder.AddVertex(
                new Vector3(0, -0.25f, 0.85f),
                new Vector3(0, 0.25f, -0.25f),
                new Vector2(0, 0));
            int f = builder.AddVertex(
                new Vector3(1, -0.25f, 0.85f),
                new Vector3(0, 0.25f, -0.25f), 
                new Vector2(1, 0));
            int g = builder.AddVertex(
                new Vector3(0, 0f, 1),
                new Vector3(0, 0.25f, -0.25f), 
                new Vector2(0, 1));
            int h = builder.AddVertex(
                new Vector3(1, 0f, 1),
                new Vector3(0, 0.25f, -0.25f), 
                new Vector2(1, 1));
            builder.AddQuad(e, g, h, f);
        }
        if (!tile.GetNeighbourProperty(6, GridTileProperty.Water)) // Bottom
        {
            builder.VertexMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(180, Vector3.up)) *
                                   Matrix4x4.Translate(new Vector3(-1, 0, -1));
            int e = builder.AddVertex(
                new Vector3(0, -0.25f, 0.85f),
                new Vector3(0, 0.25f, -0.25f),
                new Vector2(0, 0));
            int f = builder.AddVertex(
                new Vector3(1, -0.25f, 0.85f),
                new Vector3(0, 0.25f, -0.25f), 
                new Vector2(1, 0));
            int g = builder.AddVertex(
                new Vector3(0, 0f, 1),
                new Vector3(0, 0.25f, -0.25f), 
                new Vector2(0, 1));
            int h = builder.AddVertex(
                new Vector3(1, 0f, 1),
                new Vector3(0, 0.25f, -0.25f), 
                new Vector2(1, 1));
            builder.AddQuad(e, g, h, f);
        }
        // - END - Water Walls
        
        // - START - Water Corner Walls
        builder.VertexMatrix = Matrix4x4.identity;
        // Set the corner wall textures to only use a triangle. Was advised to do this by Cecilia.
        builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.5f, 0.3f, 1.0f)) *
                                Matrix4x4.Scale(new Vector3(0.5f, 0.2f, 1.0f));
        // TODO: Fix the walls so the TextureMatrix is rotated correctly for all the corners.
        // Right + Top corner wall.
        if (tile.GetNeighbourProperty(0, GridTileProperty.Water) && tile.GetNeighbourProperty(2, GridTileProperty.Water) && !tile.GetNeighbourProperty(1, GridTileProperty.Water))
        {
            builder.VertexMatrix = Matrix4x4.identity;
            int e = builder.AddVertex(
                new Vector3(1f, -0.25f, 0.85f),
                new Vector3(-0.25f, 0.25f, -0.25f),
                new Vector2(0, 1));
            int f = builder.AddVertex(
                new Vector3(1, 0f, 1),
                new Vector3(-0.25f, 0.25f, -0.25f), 
                new Vector2(0.5f, 0.5f));
            int g = builder.AddVertex(
                new Vector3(0.85f, -0.25f, 1),
                new Vector3(-0.25f, 0.25f, -0.25f), 
                new Vector2(0, 0));
            builder.AddTriangle(e, g, f);
        }
        // Right + Bottom corner wall.
        if (tile.GetNeighbourProperty(0, GridTileProperty.Water) && tile.GetNeighbourProperty(6, GridTileProperty.Water) && !tile.GetNeighbourProperty(7, GridTileProperty.Water))
        {
            int e = builder.AddVertex(
                new Vector3(0.85f, -0.25f, 0),
                new Vector3(-0.25f, 0.25f, 0.25f),
                new Vector2(0, 1));
            int f = builder.AddVertex(
                new Vector3(1, -0.25f, 0.15f),
                new Vector3(-0.25f, 0.25f, 0.25f), 
                new Vector2(0, 0));
            int h = builder.AddVertex(
                new Vector3(1, 0f, 0),
                new Vector3(-0.25f, 0.25f, 0.25f), 
                new Vector2(0.5f, 0.5f));
            builder.AddTriangle(e, f, h);
        }
        // Left + Top corner wall.
        if (tile.GetNeighbourProperty(4, GridTileProperty.Water) && tile.GetNeighbourProperty(2, GridTileProperty.Water) && !tile.GetNeighbourProperty(3, GridTileProperty.Water))
        {
            int e = builder.AddVertex(
                new Vector3(0.15f, -0.25f, 1f),
                new Vector3(0.25f, 0.25f, -0.25f),
                new Vector2(0, 1));
            int f = builder.AddVertex(
                new Vector3(0, 0f, 1),
                new Vector3(0.25f, 0.25f, -0.25f), 
                new Vector2(0.5f, 0.5f));
            int g = builder.AddVertex(
                new Vector3(0, -0.25f, 0.85f),
                new Vector3(0.25f, 0.25f, -0.25f), 
                new Vector2(0, 0));
            builder.AddTriangle(e, g, f);
        }
        // Left + Bottom corner wall.
        if (tile.GetNeighbourProperty(4, GridTileProperty.Water) && tile.GetNeighbourProperty(6, GridTileProperty.Water) && !tile.GetNeighbourProperty(5, GridTileProperty.Water))
        {
            int e = builder.AddVertex(
                new Vector3(0, -0.25f, 0.15f),
                new Vector3(0.25f, 0.25f, 0.25f),
                new Vector2(0, 1));
            int f = builder.AddVertex(
                new Vector3(0, 0f, 0),
                new Vector3(0.25f, 0.25f, 0.25f), 
                new Vector2(0.5f, 0.5f));
            int g = builder.AddVertex(
                new Vector3(0.15f, -0.25f, 0),
                new Vector3(0.25f, 0.25f, 0.25f), 
                new Vector2(0, 0));
            builder.AddTriangle(e, g, f);
        }
        // - END - Water Corner Walls
        
        builder.Build(mesh);
    }

    private void SetRoad(MeshBuilder builder)
    {
        int a, b, c, d;

        // - START - Road connections
        if (tile.GetNeighbourProperty(0, GridTileProperty.Solid) && tile.GetNeighbourProperty(6, GridTileProperty.Solid))
        {
            builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0f, 0f, 1.0f)) *
                                    Matrix4x4.Scale(new Vector3(1f, 0.5f, 1.0f));
            
            builder.VertexMatrix = Matrix4x4.identity *
                                   Matrix4x4.Translate(new Vector3(1, 0, 0)) *
                                   Matrix4x4.Rotate(Quaternion.AngleAxis(90, Vector3.down));
            a = builder.AddVertex(
                new Vector3(0, 0.0001f, 0),
                Vector3.up,
                new Vector2(0.5f, 0f));
            b = builder.AddVertex(
                new Vector3(1, 0.0001f, 0),
                Vector3.up, 
                new Vector2(0f, 0f));
            c = builder.AddVertex(
                new Vector3(0, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0f, 0f));
            d = builder.AddVertex(
                new Vector3(1, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0f, 0f));
        }
        else if (tile.GetNeighbourProperty(4, GridTileProperty.Solid) && tile.GetNeighbourProperty(6, GridTileProperty.Solid)) {
                builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0f, 0f, 1.0f)) *
                                        Matrix4x4.Scale(new Vector3(1f, 0.5f, 1.0f));
            
                builder.VertexMatrix = Matrix4x4.identity *
                                       Matrix4x4.Translate(new Vector3(0, 0, 0)) *
                                       Matrix4x4.Rotate(Quaternion.AngleAxis(0, Vector3.down));
                a = builder.AddVertex(
                    new Vector3(0, 0.0001f, 0),
                    Vector3.up,
                    new Vector2(0.5f, 0f));
                b = builder.AddVertex(
                    new Vector3(1, 0.0001f, 0),
                    Vector3.up, 
                    new Vector2(0f, 0f));
                c = builder.AddVertex(
                    new Vector3(0, 0.0001f, 1),
                    Vector3.up, 
                    new Vector2(0f, 0f));
                d = builder.AddVertex(
                    new Vector3(1, 0.0001f, 1),
                    Vector3.up, 
                    new Vector2(0f, 0f));
        }
        else if (tile.GetNeighbourProperty(0, GridTileProperty.Solid) && tile.GetNeighbourProperty(2, GridTileProperty.Solid))
        {
            builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0f, 0f, 1.0f)) *
                                    Matrix4x4.Scale(new Vector3(1f, 0.5f, 1.0f));
            
            builder.VertexMatrix = Matrix4x4.identity *
                                   Matrix4x4.Translate(new Vector3(0, 0, 0)) *
                                   Matrix4x4.Rotate(Quaternion.AngleAxis(0, Vector3.down));
            a = builder.AddVertex(
                new Vector3(0, 0.0001f, 0),
                Vector3.up,
                new Vector2(0.5f, 0f));
            b = builder.AddVertex(
                new Vector3(1, 0.0001f, 0),
                Vector3.up, 
                new Vector2(0.5f, 0f));
            c = builder.AddVertex(
                new Vector3(0, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0.5f, 0f));
            d = builder.AddVertex(
                new Vector3(1, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0f, 0f));
        }
        else if (tile.GetNeighbourProperty(4, GridTileProperty.Solid) && tile.GetNeighbourProperty(2, GridTileProperty.Solid))
        {
            builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0f, 0f, 1.0f)) *
                                    Matrix4x4.Scale(new Vector3(1f, 0.5f, 1.0f));
            
            builder.VertexMatrix = Matrix4x4.identity *
                                   Matrix4x4.Translate(new Vector3(1, 0, 0)) *
                                   Matrix4x4.Rotate(Quaternion.AngleAxis(90, Vector3.down));
            a = builder.AddVertex(
                new Vector3(0, 0.0001f, 0),
                Vector3.up,
                new Vector2(0.5f, 0f));
            b = builder.AddVertex(
                new Vector3(1, 0.0001f, 0),
                Vector3.up, 
                new Vector2(0.5f, 0f));
            c = builder.AddVertex(
                new Vector3(0, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0.5f, 0f));
            d = builder.AddVertex(
                new Vector3(1, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0f, 0f));
        }
        else if (tile.GetNeighbourProperty(4, GridTileProperty.Solid) || tile.GetNeighbourProperty(0, GridTileProperty.Solid))
        {
            builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0f, 0f, 1.0f)) *
                                    Matrix4x4.Scale(new Vector3(1f, 0.5f, 1.0f));
            
            builder.VertexMatrix = Matrix4x4.identity *
                                   Matrix4x4.Translate(new Vector3(0, 0, 0)) *
                                   Matrix4x4.Rotate(Quaternion.AngleAxis(0, Vector3.down));
            a = builder.AddVertex(
                new Vector3(0, 0.0001f, 0),
                Vector3.up,
                new Vector2(0.5f, 0f));
            b = builder.AddVertex(
                new Vector3(1, 0.0001f, 0),
                Vector3.up, 
                new Vector2(0.5f, 0f));
            c = builder.AddVertex(
                new Vector3(0, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0f, 0f));
            d = builder.AddVertex(
                new Vector3(1, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0f, 0f));
        }
        else
        {
            builder.TextureMatrix = Matrix4x4.Translate(new Vector3(1f, 0.5f, 1.0f)) *
                                    Matrix4x4.Scale(new Vector3(1.0f, 0.5f, 1.0f));
            a = builder.AddVertex(
                new Vector3(0, 0.0001f, 0),
                Vector3.up,
                new Vector2(0.5f, 0.85f));
            b = builder.AddVertex(
                new Vector3(1, 0.0001f, 0),
                Vector3.up, 
                new Vector2(0, 0.85f));
            c = builder.AddVertex(
                new Vector3(0, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0.5f, 0.85f));
            d = builder.AddVertex(
                new Vector3(1, 0.0001f, 1),
                Vector3.up, 
                new Vector2(0, 0.85f)); 
        }
        // - END - Road connections
        // Reset all rotations and translations done during the earlier steps.
        builder.VertexMatrix = Matrix4x4.identity;


        builder.AddQuad(a, c, d, b);

        builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.5f, 0.5f, 1.0f)) *
                                Matrix4x4.Scale(new Vector3(0.5f, 0.5f, 1.0f));
        // Check for if we should have water or grass
        if (!tile.GetProperty(GridTileProperty.Water)) // All of this can probably be changed to just call the "SetGrass" method. If I changed the build to not be part of it.
        {
            int g = builder.AddVertex(
                new Vector3(0, 0, 0),
                Vector3.up,
                new Vector2(0.5f, 0.5f));
            int h = builder.AddVertex(
                new Vector3(1, 0, 0),
                Vector3.up, 
                new Vector2(0.5f, 0.5f));
            int i = builder.AddVertex(
                new Vector3(0, 0, 1),
                Vector3.up, 
                new Vector2(0.5f, 0.5f));
            int j = builder.AddVertex(
                new Vector3(1, 0, 1),
                Vector3.up, 
                new Vector2(0.5f, 0.5f));
        
            builder.AddQuad(g, i, j, h);
            builder.Build(mesh);
        }
        else
        {
            if (tile.GetNeighbourProperty(0, GridTileProperty.Solid) && tile.GetNeighbourProperty(2, GridTileProperty.Solid)) // Change
            {
                // Left turn coming from the top.
                builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.14f, 0.14f, 1.0f)) *
                                        Matrix4x4.Scale(new Vector3(0.1f, 0.1f, 1.0f));
                // Add depth to the bridge.
                int g = builder.AddVertex(
                    new Vector3(1f, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 0));
                int h = builder.AddVertex(
                    new Vector3(0.1f, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 1));
                int i = builder.AddVertex(
                    new Vector3(0.1f, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(1, 1));
                int j = builder.AddVertex(
                    new Vector3(1f, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(1, 0));
                builder.AddQuad(g, h, i, j); // Z- axies
                
                int k = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 0));
                int l = builder.AddVertex(
                    new Vector3(1f, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 1));
                int m = builder.AddVertex(
                    new Vector3(1f, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(1, 1));
                int n = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(1, 0));
                builder.AddQuad(k, l, m, n); // Z+ axies
                
                int kb = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 1f),
                    Vector3.right,
                    new Vector2(0, 0));
                int lb = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0.9f),
                    Vector3.right,
                    new Vector2(0, 1));
                int mb = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0.9f),
                    Vector3.right,
                    new Vector2(1, 1));
                int nb = builder.AddVertex(
                    new Vector3(0.9f, 0f, 1f),
                    Vector3.right,
                    new Vector2(1, 0));
                builder.AddQuad(kb, lb, mb, nb); // X+ axies
                
                int gb = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 0.1f),
                    Vector3.left,
                    new Vector2(0, 0));
                int hb = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 1f),
                    Vector3.left,
                    new Vector2(0, 1));
                int ib = builder.AddVertex(
                    new Vector3(0.095f, 0f, 1f),
                    Vector3.left,
                    new Vector2(1, 1));
                int jb = builder.AddVertex(
                    new Vector3(0.095f, 0f, 0.1f),
                    Vector3.left,
                    new Vector2(1, 0));
                builder.AddQuad(gb, hb, ib, jb); // X- axies
            }
            else if (tile.GetNeighbourProperty(6, GridTileProperty.Solid) && tile.GetNeighbourProperty(4, GridTileProperty.Solid))
            {
                // Left turn coming from the bottom.
                builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.14f, 0.14f, 1.0f)) *
                                        Matrix4x4.Scale(new Vector3(0.1f, 0.1f, 1.0f));
                // Add depth to the bridge.
                int g = builder.AddVertex(
                    new Vector3(0.1f, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 0));
                int h = builder.AddVertex(
                    new Vector3(0, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 1));
                int i = builder.AddVertex(
                    new Vector3(0, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(1, 1));
                int j = builder.AddVertex(
                    new Vector3(0.1f, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(1, 0));
                builder.AddQuad(g, h, i, j); // Z- axies
                
                int k = builder.AddVertex(
                    new Vector3(0, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 0));
                int l = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 1));
                int m = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(1, 1));
                int n = builder.AddVertex(
                    new Vector3(0, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(1, 0));
                builder.AddQuad(k, l, m, n); // Z+ axies
                
                int kb = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0.9f),
                    Vector3.right,
                    new Vector2(0, 0));
                int lb = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0),
                    Vector3.right,
                    new Vector2(0, 1));
                int mb = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0),
                    Vector3.right,
                    new Vector2(1, 1));
                int nb = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0.9f),
                    Vector3.right,
                    new Vector2(1, 0));
                builder.AddQuad(kb, lb, mb, nb); // X+ axies
                
                int gb = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 0),
                    Vector3.left,
                    new Vector2(0, 0));
                int hb = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 0.1f),
                    Vector3.left,
                    new Vector2(0, 1));
                int ib = builder.AddVertex(
                    new Vector3(0.095f, 0f, 0.1f),
                    Vector3.left,
                    new Vector2(1, 1));
                int jb = builder.AddVertex(
                    new Vector3(0.095f, 0f, 0),
                    Vector3.left,
                    new Vector2(1, 0));
                builder.AddQuad(gb, hb, ib, jb); // X- axies
                
            }
            else if (tile.GetNeighbourProperty(0, GridTileProperty.Solid) && tile.GetNeighbourProperty(6, GridTileProperty.Solid))
            {
                // Right turn coming from the bottom.
                builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.14f, 0.14f, 1.0f)) *
                                        Matrix4x4.Scale(new Vector3(0.1f, 0.1f, 1.0f));
                // Add depth to the bridge.
                int g = builder.AddVertex(
                    new Vector3(1f, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 0));
                int h = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 1));
                int i = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(1, 1));
                int j = builder.AddVertex(
                    new Vector3(1f, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 1));
                builder.AddQuad(g, h, i, j); // Z- axies
                
                int k = builder.AddVertex(
                    new Vector3(0.1f, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 0));
                int l = builder.AddVertex(
                    new Vector3(1f, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 1));
                int m = builder.AddVertex(
                    new Vector3(1f, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(1, 1));
                int n = builder.AddVertex(
                    new Vector3(0.1f, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 1));
                builder.AddQuad(k, l, m, n); // Z+ axies
                
                int kb = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0.1f),
                    Vector3.right,
                    new Vector2(0, 0));
                int lb = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0),
                    Vector3.right,
                    new Vector2(1, 0));
                int mb = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0),
                    Vector3.right,
                    new Vector2(1, 1));
                int nb = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0.1f),
                    Vector3.right,
                    new Vector2(0, 1));
                builder.AddQuad(kb, lb, mb, nb); // X+ axies
                
                int gb = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 0f),
                    Vector3.left,
                    new Vector2(0, 0));
                int hb = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 0.9f),
                    Vector3.left,
                    new Vector2(1, 0));
                int ib = builder.AddVertex(
                    new Vector3(0.095f, 0f, 0.9f),
                    Vector3.left,
                    new Vector2(1, 1));
                int jb = builder.AddVertex(
                    new Vector3(0.095f, 0f, 0f),
                    Vector3.left,
                    new Vector2(0, 1));
                builder.AddQuad(gb, hb, ib, jb); // X- axies
            }
            else if (tile.GetNeighbourProperty(2, GridTileProperty.Solid) && tile.GetNeighbourProperty(4, GridTileProperty.Solid)) // Change
            {
                // Right turn coming from the top.
                builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.14f, 0.14f, 1.0f)) *
                                        Matrix4x4.Scale(new Vector3(0.1f, 0.1f, 1.0f));
                // Add depth to the bridge.
                int g = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 0));
                int h = builder.AddVertex(
                    new Vector3(0f, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(1, 0));
                int i = builder.AddVertex(
                    new Vector3(0f, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(1, 1));
                int j = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 1));
                builder.AddQuad(g, h, i, j); // Z- axies
                
                int k = builder.AddVertex(
                    new Vector3(0f, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 0));
                int l = builder.AddVertex(
                    new Vector3(0.1f, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(1, 0));
                int m = builder.AddVertex(
                    new Vector3(0.1f, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(1, 1));
                int n = builder.AddVertex(
                    new Vector3(0f, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 1));
                builder.AddQuad(k, l, m, n); // Z+ axies
                
                int kb = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 1f),
                    Vector3.right,
                    new Vector2(0, 0));
                int lb = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0.1f),
                    Vector3.right,
                    new Vector2(1, 0));
                int mb = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0.1f),
                    Vector3.right,
                    new Vector2(1, 1));
                int nb = builder.AddVertex(
                    new Vector3(0.9f, 0f, 1f),
                    Vector3.right,
                    new Vector2(0, 1));
                builder.AddQuad(kb, lb, mb, nb); // X+ axies
                
                int gb = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 0.9f),
                    Vector3.left,
                    new Vector2(0, 0));
                int hb = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 1f),
                    Vector3.left,
                    new Vector2(1, 0));
                int ib = builder.AddVertex(
                    new Vector3(0.095f, 0f, 1f),
                    Vector3.left,
                    new Vector2(1, 1));
                int jb = builder.AddVertex(
                    new Vector3(0.095f, 0f, 0.9f),
                    Vector3.left,
                    new Vector2(0, 1));
                builder.AddQuad(gb, hb, ib, jb); // X- axies
            }
            else if (tile.GetNeighbourProperty(0, GridTileProperty.Solid) || tile.GetNeighbourProperty(4, GridTileProperty.Solid))
            {
                builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.14f, 0.14f, 1.0f)) *
                                        Matrix4x4.Scale(new Vector3(0.1f, 0.1f, 1.0f));
                // Add depth to the bridge.
                int g = builder.AddVertex(
                    new Vector3(1, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 0));
                int h = builder.AddVertex(
                    new Vector3(0, -0.02f, 0.095f),
                    Vector3.back,
                    new Vector2(1, 0));
                int i = builder.AddVertex(
                    new Vector3(0, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(1, 1));
                int j = builder.AddVertex(
                    new Vector3(1, 0f, 0.095f),
                    Vector3.back,
                    new Vector2(0, 1));
                builder.AddQuad(g, h, i, j);
                int k = builder.AddVertex(
                    new Vector3(0, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 0));
                int l = builder.AddVertex(
                    new Vector3(1, -0.02f, 0.9f),
                    Vector3.forward,
                    new Vector2(1, 0));
                int m = builder.AddVertex(
                    new Vector3(1, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(1, 1));
                int n = builder.AddVertex(
                    new Vector3(0, 0f, 0.9f),
                    Vector3.forward,
                    new Vector2(0, 1));
                builder.AddQuad(k, l, m, n);
            }
            else
            {
                builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.14f, 0.14f, 1.0f)) *
                                        Matrix4x4.Scale(new Vector3(0.1f, 0.1f, 1.0f));
                // Add depth to the bridge.
                int g = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 0),
                    Vector3.left,
                    new Vector2(0, 0));
                int h = builder.AddVertex(
                    new Vector3(0.095f, -0.02f, 1),
                    Vector3.left,
                    new Vector2(1, 0));
                int i = builder.AddVertex(
                    new Vector3(0.095f, 0f, 1),
                    Vector3.left,
                    new Vector2(1, 1));
                int j = builder.AddVertex(
                    new Vector3(0.095f, 0f, 0),
                    Vector3.left,
                    new Vector2(0, 1));
                builder.AddQuad(g, h, i, j);
                int k = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 1),
                    Vector3.right,
                    new Vector2(0, 0));
                int l = builder.AddVertex(
                    new Vector3(0.9f, -0.02f, 0),
                    Vector3.right,
                    new Vector2(1, 0));
                int m = builder.AddVertex(
                    new Vector3(0.9f, 0f, 0),
                    Vector3.right,
                    new Vector2(1, 1));
                int n = builder.AddVertex(
                    new Vector3(0.9f, 0f, 1),
                    Vector3.right,
                    new Vector2(0, 1));
                builder.AddQuad(k, l, m, n);
            }
            
            // Left side of the pillar
            int o = builder.AddVertex(
                new Vector3(0.4f, -0.3f, 0.4f),
                Vector3.left,
                new Vector2(0, 0));
            int p = builder.AddVertex(
                new Vector3(0.4f, -0.3f, 0.6f),
                Vector3.left,
                new Vector2(1, 0));
            int q = builder.AddVertex(
                new Vector3(0.4f, 0f, 0.6f),
                Vector3.left,
                new Vector2(1, 1));
            int r = builder.AddVertex(
                new Vector3(0.4f, 0f, 0.4f),
                Vector3.left,
                new Vector2(0, 1));
            builder.AddQuad(o, p, q, r);
            
            // Back side of the pillar
            int ob = builder.AddVertex(
                new Vector3(0.6f, -0.3f, 0.4f),
                Vector3.back,
                new Vector2(0, 0));
            int pb = builder.AddVertex(
                new Vector3(0.4f, -0.3f, 0.4f),
                Vector3.back,
                new Vector2(1, 0));
            int qb = builder.AddVertex(
                new Vector3(0.4f, 0f, 0.4f),
                Vector3.back,
                new Vector2(1, 1));
            int rb = builder.AddVertex(
                new Vector3(0.6f, 0f, 0.4f),
                Vector3.back,
                new Vector2(0, 1));
            builder.AddQuad(ob, pb, qb, rb);
            
            // Right side of the pillar
            int oc = builder.AddVertex(
                new Vector3(0.6f, -0.3f, 0.6f),
                Vector3.right,
                new Vector2(0, 0));
            int pc = builder.AddVertex(
                new Vector3(0.6f, -0.3f, 0.4f),
                Vector3.right,
                new Vector2(1, 0));
            int qc = builder.AddVertex(
                new Vector3(0.6f, 0f, 0.4f),
                Vector3.right,
                new Vector2(1, 1));
            int rc = builder.AddVertex(
                new Vector3(0.6f, 0f, 0.6f),
                Vector3.right,
                new Vector2(0, 1));
            builder.AddQuad(oc, pc, qc, rc);
            
            // Forward side of the pillar
            int od = builder.AddVertex(
                new Vector3(0.4f, -0.3f, 0.6f),
                Vector3.forward,
                new Vector2(0, 0));
            int pd = builder.AddVertex(
                new Vector3(0.6f, -0.3f, 0.6f),
                Vector3.forward,
                new Vector2(1, 0));
            int qd = builder.AddVertex(
                new Vector3(0.6f, 0f, 0.6f),
                Vector3.forward,
                new Vector2(1, 1));
            int rd = builder.AddVertex(
                new Vector3(0.4f, 0f, 0.6f),
                Vector3.forward,
                new Vector2(0, 1));
            builder.AddQuad(od, pd, qd, rd);

            // Add water and finish the building process.
            SetWater(builder);
        }
    }

    private void SetGrass(MeshBuilder builder)
    {
        builder.TextureMatrix = Matrix4x4.Translate(new Vector3(0.52f, 0.52f, 1f)) *
                                Matrix4x4.Scale(new Vector3(0.475f, 0.475f, 1f)); // Changed to fit with texture padding. This is to fix the problem with the texture borders bleeding over.
        int a = builder.AddVertex(
            new Vector3(0, 0, 0),
            Vector3.up,
            new Vector2(0f, 0f));
        int b = builder.AddVertex(
            new Vector3(1, 0, 0),
            Vector3.up, 
            new Vector2(1f, 0f));
        int c = builder.AddVertex(
            new Vector3(0, 0, 1),
            Vector3.up, 
            new Vector2(0f, 1f));
        int d = builder.AddVertex(
            new Vector3(1, 0, 1),
            Vector3.up, 
            new Vector2(1f, 1f));
        
        builder.AddQuad(a, c, d, b);
        builder.Build(mesh);
    }
}
