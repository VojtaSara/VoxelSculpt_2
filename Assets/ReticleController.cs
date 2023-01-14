using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.XR;

public class ReticleController : MonoBehaviour
{
    private InputData _inputData;
    GameObject voxelGrid;
    private float scale;
    private float epsilon = 0.0001f;

    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<InputData>();
        voxelGrid = GameObject.FindWithTag("VoxelGrid");
        scale = voxelGrid.GetComponent<VoxelRender>().getGlobalScale();
        // set the scale of this object to be the same as the scale of the voxel grid
        transform.localScale = new Vector3(scale + epsilon, scale + epsilon, scale + epsilon);
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPosition))
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion controllerRotation))
            {
                if (this.GetComponent<DrawingParamaters>().drawingEnabled)
                {
                    transform.position = voxelGrid
                        .GetComponent<VoxelRender>()
                        .realCoordsToGridCoords(controllerPosition, controllerRotation)
                        * scale;
                }
                else
                {
                    transform.position = new Vector3(-10f, -10f, -10f);
                }
            }
        }
    }
}
