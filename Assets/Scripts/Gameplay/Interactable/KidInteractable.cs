using UnityEngine;
using System.Collections;

public class KidInteractable : Interactable
{
    public KidInteractable()
    {
        m_GameEvent = EGameEvent.CHILD_COLLISION;
        m_IsPassableThrough = true;
    }
}
