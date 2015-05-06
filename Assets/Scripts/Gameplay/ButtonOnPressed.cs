using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//TODO: Find a better name
public class ButtonOnPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool mButtonHeld;

    public void OnPointerDown(PointerEventData pEventData)
    {
        mButtonHeld = true;
    }

    public void OnPointerUp(PointerEventData pEventData)
    {
        mButtonHeld = false;
    }

    public bool IsPressed
    {
        get { return mButtonHeld; }
    }
}
