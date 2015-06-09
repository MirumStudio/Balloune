/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
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
