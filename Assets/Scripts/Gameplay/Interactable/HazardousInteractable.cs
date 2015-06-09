/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;

public class HazardousInteractable : Interactable
{
    [SerializeField]
    private int m_Damage = 1;

    public HazardousInteractable()
    {
        m_GameEvent = EGameEvent.HAZARDOUS_COLLISION;
    }

    public int Damage
    {
        get { return m_Damage; }
    }
}
