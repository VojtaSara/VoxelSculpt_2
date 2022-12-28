using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer), typeof(InputData))]
public class VoxelRender : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;
    VoxelDataHandler voxelDataHandler;

    private InputData _inputData;

    public float scale = 1f;

    float adjScale;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjScale = scale * 0.5f;
    }

    void Start()
    {
        _inputData = GetComponent<InputData>();

        voxelDataHandler = new VoxelDataHandler(adjScale, _inputData);
        GenerateVoxelMesh(voxelDataHandler.data);
        UpdateMesh();
    }

    void Update()
    {
        voxelDataHandler.Update();
        GenerateVoxelMesh(voxelDataHandler.data);
        UpdateMesh();
    }

    private void GenerateVoxelMesh(VoxelData data)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int z = 0; z < data.Height; z++)
        {
            for (int y = 0; y < data.Depth; y++)
            {
                for (int x = 0; x < data.Width; x++)
                {
                    if (data.GetCell(x, y, z) == 0)
                    {
                        continue;
                    }
                    MakeCube(adjScale, new Vector3((float)x * scale, (float)y * scale, (float)z * scale), x, y, z, data);
                }
            }
        }
    }
    void MakeCube(float cubeScale, Vector3 cubePos, int x, int y, int z, VoxelData data)
    {
        for (int i = 0; i < 6; i++)
        {
            if (data.GetNeighbour(x,y,z,(Direction)i) == 0)
            {
                MakeFace((Direction)i, cubeScale, cubePos);
            }
        }
    }

    void MakeFace(Direction dir, float faceScale, Vector3 facePos)
    {
        vertices.AddRange(CubeMeshData.faceVertices(dir, faceScale, facePos));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

}
