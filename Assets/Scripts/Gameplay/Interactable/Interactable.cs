/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;

public class Interactable : MonoBehaviour {

    [SerializeField]
    protected EGameEvent m_GameEvent;

    [SerializeField]
    protected bool m_IsPassableThrough = false;

    public void DispacthEvent()
    {
        EventService.DispatchEvent(m_GameEvent, this);
    }

    public bool IsPassableThrough
    {
        get { return m_IsPassableThrough; }
    }
}
