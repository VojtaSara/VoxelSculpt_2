using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    GameObject voxelGrid;
    public int selectedColor = 0; 
    // Start is called before the first frame update
    void Start()
    {
        voxelGrid = GameObject.FindWithTag("VoxelGrid");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSelectedColor(int color)
    {
        selectedColor = color;
        voxelGrid.GetComponent<VoxelRender>().stopRecievingInput();
        voxelGrid.GetComponent<VoxelRender>().setMaterial(color);

    }    
}
