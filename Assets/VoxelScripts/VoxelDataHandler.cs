﻿using System;
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
    private Vector3 lastPos;
    private int selectedMat;

    public VoxelDataHandler(float scale, InputData inputData)
    {
        this.data = new VoxelData();
        data.ChangeData(1, 1, 1, 1);
        data.ChangeData(2, 2, 2, 2);
        data.ChangeData(3, 3, 3, 1);
        data.ChangeData(4, 4, 4, 2);
        data.ChangeData(5, 5, 5, 1);
        data.ChangeData(6, 6, 6, 2);
        data.ChangeData(7, 7, 7, 2);
        data.ChangeData(8, 8, 8, 2);
        data.ChangeData(9, 9, 9, 2);
        data.ChangeData(10, 10, 10, 1);
        this.GlobalSceneScale = scale;
        this._inputData = inputData;
        this.selectedMat = 1;
    }

    public void Update()
    {
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isPressed) && isPressed)
        {
            selectedMat = (selectedMat == 1) ? 2 : 1;
        }


        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPosition))
        {
            if (_inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out float triggerPos))
            {
                if (triggerPos > 0.5f && realCoordsToGridCoords(controllerPosition) != lastPos)
                {
                    Vector3Int voxelPos = realCoordsToGridCoords(controllerPosition);
                    lastPos = voxelPos;
                    if (data.GetCell(voxelPos.x, voxelPos.y, voxelPos.z) == 0)
                    {
                        data.ChangeData(voxelPos.x, voxelPos.y, voxelPos.z, selectedMat);
                    }
                    else
                    {
                        data.ChangeData(voxelPos.x, voxelPos.y, voxelPos.z, 0);
                    }
                }
            }
            System.Random r = new System.Random();
/*          Vector3 GridCoords = realCoordsToGridCoords(rightHandCoords());

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
