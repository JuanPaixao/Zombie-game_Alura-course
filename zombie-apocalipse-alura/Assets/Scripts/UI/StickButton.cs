using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems; // to use interface Drag Handler

public class StickButton : MonoBehaviour, IDragHandler //unity EventSystems interface to use OnDrag methoid
{
    [SerializeField] private RectTransform _backgroundImage; //my button background image
    [SerializeField] private RectTransform _stickImage; //my "stick" image
    [SerializeField] private Vector2UnityEvent _OnValueChanged;


    public void OnDrag(PointerEventData eventData) //my drag function
    {
        Vector2 mousePosition = CalculateMousePosition(eventData); //calculating the mouse position 
        Vector2 limitedPosition = this.LimitedPosition(mousePosition); //limit my mousePosition
        this.SetJoystickPosition(limitedPosition); //setting the stick/joystick position with this mouse position
        _OnValueChanged.Invoke(limitedPosition); //invoke my function placed on my unity event when i move my stick
    }
    private Vector2 CalculateMousePosition(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroundImage, eventData.position, eventData.enterEventCamera, out position);
        //transforming my screenpoint to my point in rectangle (image rectangle)
        return position; //return it
    }
    private void SetJoystickPosition(Vector2 mousePosition)
    {
        this._stickImage.localPosition = mousePosition * ImageSize(); //setting the localposition = mouseposition, otherwise it will use global position of the canvas
    }

    private Vector2 LimitedPosition(Vector2 mousePosition)
    {
        Vector2 limitedPos = mousePosition / this.ImageSize(); //my mouse position / my background image size
        if (limitedPos.magnitude > 1)
        {
            limitedPos = limitedPos.normalized; // if the result is != 1, then my mousePosition is bigger than my background image, so o force it to be 1;
        }
        return limitedPos; //return it
    }

    private float ImageSize()
    {
        return this._backgroundImage.rect.width / 2; //my reference image size, as it is a square, i choose the width because it doesnt make difference in this case, and i % by 2 because
                                                     //my reference is the CENTER of the image, not the edge
    }
}

[Serializable] public class Vector2UnityEvent : UnityEvent<Vector2> //using serializable in order to unity serialize it and show it on my editor
{

}

