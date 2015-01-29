using UnityEngine;
using System.Collections;

public class HazardousInteractable : Interactable
{
    public HazardousInteractable()
    {
        m_GameEvent = EGameEvent.HAZARDOUS_COLLISION;
    }
}
