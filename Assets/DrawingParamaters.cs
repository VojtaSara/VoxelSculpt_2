using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingParamaters : MonoBehaviour
{
    public bool drawingEnabled = true;
    private VoxelRender voxelGrid;

    void Start()
    {
        voxelGrid = GameObject.FindWithTag("VoxelGrid").GetComponent<VoxelRender>();
    }

    public void disableDrawing()
    {
        voxelGrid.stopRecievingInput();
        drawingEnabled = false;
    }

    public void enableDrawing()
    {
        voxelGrid.startRecievingInput();
        drawingEnabled = true;
    }

    public void switchMode(string mode)
    {
        voxelGrid.switchMode(mode);
    }
}
