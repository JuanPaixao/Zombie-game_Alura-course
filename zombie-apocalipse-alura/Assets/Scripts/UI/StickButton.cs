using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // to use interface Drag Handler

public class StickButton : MonoBehaviour, IDragHandler //unity EventSystems interface to use OnDrag methoid
{
    [SerializeField] private RectTransform _backgroundImage; //my button background image
    [SerializeField] private RectTransform _stickImage; //my "stick" image
    public void OnDrag(PointerEventData eventData) //my drag function
    {
        Vector2 mousePosition = CalculateMousePosition(eventData); //calculating the mouse position 
        this.SetJoystickPosition(mousePosition); //setting the stick/joystick position with this mouse position
        Debug.Log(mousePosition);
    }

    private void SetJoystickPosition(Vector2 mousePosition) 
    {
       this._stickImage.localPosition = mousePosition; //setting the localposition = mouseposition, otherwise it will use global position of the canvas
    }

    private Vector2 CalculateMousePosition(PointerEventData eventData)
    {
        Vector2 position; 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroundImage, eventData.position, eventData.enterEventCamera, out position);
		//transforming my screenpoint to my point in rectangle (image rectangle)
        return position; //return it
    }
}