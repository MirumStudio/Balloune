/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

public class KidInteractable : Interactable
{
    public KidInteractable()
    {
        m_GameEvent = EGameEvent.CHILD_COLLISION;
        m_IsPassableThrough = true;
    }
}
