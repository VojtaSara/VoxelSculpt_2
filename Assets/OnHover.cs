using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    GameObject drawingParameters;
    public bool isPaletteButton = true;

    void Start()
    {
        drawingParameters = GameObject.FindWithTag("DrawingParameters");
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPaletteButton)
        {
            drawingParameters.GetComponent<DrawingParamaters>().switchMode("draw");
        }
    }

    void OnHoverEnter()
    {
        // make the button color lighter
        drawingParameters.GetComponent<DrawingParamaters>().disableDrawing();
    }

    void OnHoverExit()
    {
        drawingParameters.GetComponent<DrawingParamaters>().enableDrawing();
    }


}