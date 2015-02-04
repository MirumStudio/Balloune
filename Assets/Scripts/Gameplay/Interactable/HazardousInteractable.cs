using UnityEngine;
using System.Collections;

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
