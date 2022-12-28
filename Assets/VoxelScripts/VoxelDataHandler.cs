using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VoxelDataHandler
{
    public VoxelData data;
    float GlobalSceneScale;
    private InputData _inputData;
    private Vector3 controllerPosition;

    public VoxelDataHandler(float scale, InputData inputData)
    {
        this.data = new VoxelData();
        data.ChangeData(2, 2, 2, 1);
        data.ChangeData(2, 2, 4, 1);
        this.GlobalSceneScale = scale;
        this._inputData = inputData;
    }

    public void Update()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPosition))
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out float triggerPos))
            {
                if (triggerPos > 0.5f)
                {
                    Vector3Int voxelPos = realCoordsToGridCoords(controllerPosition);
                    if (data.GetCell(voxelPos.x, voxelPos.y, voxelPos.z) == 0)
                    {
                        data.ChangeData(voxelPos.x, voxelPos.y, voxelPos.z, 1);
                    }
                    else
                    {
                        data.ChangeData(voxelPos.x, voxelPos.y, voxelPos.z, 0);
                    }
                }
            }
            System.Random r = new System.Random();
/*            Vector3 GridCoords = realCoordsToGridCoords(rightHandCoords());

            if (data.GetCell((int)GridCoords.x, (int)GridCoords.y, (int)GridCoords.z) == 0)
            {
                data.ChangeData((int)GridCoords.x, (int)GridCoords.y, (int)GridCoords.z, 1);
            }
            else
            {
                data.ChangeData((int)GridCoords.x, (int)GridCoords.y, (int)GridCoords.z, 0);
            }*/
        }
    }

    private Vector3 rightHandCoords()
    {
        throw new NotImplementedException();
    }

    private Vector3Int realCoordsToGridCoords(Vector3 handPosition)
    {
        Vector3Int GridPosition = new Vector3Int((int)(handPosition.x / GlobalSceneScale), (int)(handPosition.y / GlobalSceneScale), (int)(handPosition.z / GlobalSceneScale));
        if (GridPosition.x < 0 || GridPosition.x >= data.Width || GridPosition.y < 0 || GridPosition.y >= data.Depth || GridPosition.z < 0 || GridPosition.z >= data.Height)
        {
            return new Vector3Int(0, 0, 0);
        }
        else
        {
            return GridPosition;
        }
    }
}
