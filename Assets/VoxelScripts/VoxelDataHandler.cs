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
    private Vector3 lastPos;
    public int selectedMat;
    public bool recievingInput;


    public VoxelDataHandler(float scale, InputData inputData)
    {
        this.data = new VoxelData();

        this.recievingInput = true;
        this.GlobalSceneScale = scale;
        this._inputData = inputData;
        this.selectedMat = 1;
    }

    public void Update()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPosition))
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out float triggerPos))
            {
                // log triggerpos
                _inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion controllerRotation);
                if (recievingInput
                    && triggerPos == 1 
                    && realCoordsToGridCoords(adjustTipPosition(controllerPosition, controllerRotation)) != lastPos)
                {
                    Vector3Int voxelPos = realCoordsToGridCoords(adjustTipPosition(controllerPosition, controllerRotation));
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
                if (triggerPos == 0)
                {
                    recievingInput = true;
                }
            }
        }
    }

    private Vector3 adjustTipPosition(Vector3 controllerPosition, Quaternion controllerRotation)
    {
        Vector3 tipPosition = controllerPosition + controllerRotation * new Vector3(0, -0.05f, 0.15f);
        return tipPosition;
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
