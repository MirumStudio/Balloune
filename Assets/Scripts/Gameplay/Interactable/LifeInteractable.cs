using UnityEngine;
using System.Collections;

public class LifeInteractable : Interactable
{
	[SerializeField]
    private int m_Life = 1;

    public LifeInteractable()
    {
        m_GameEvent = EGameEvent.LIFE_COLLISION;
        m_IsPassableThrough = true;
    }

    public int Life
    {
        get { return m_Life; }
    }
}
