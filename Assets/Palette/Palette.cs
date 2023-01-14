using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    GameObject voxelGrid;
    public int selectedColor = 0; 
    void Start()
    {
        voxelGrid = GameObject.FindWithTag("VoxelGrid");
    }


    public void changeSelectedColor(int color)
    {
        selectedColor = color;
        voxelGrid.GetComponent<VoxelRender>().setMaterial(color);
    }    
}
