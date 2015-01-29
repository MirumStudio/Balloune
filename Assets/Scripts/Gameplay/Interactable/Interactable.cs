using UnityEngine;
using System.Collections;
using Radix.Event;

public class Interactable : MonoBehaviour {

    [SerializeField]
    protected EGameEvent m_GameEvent;

    [SerializeField]
    protected bool m_IsPassableThrough = false;

    public void DispacthEvent()
    {
        EventService.DipatchEvent(m_GameEvent, this);
    }

    public bool IsPassableThrough
    {
        get { return m_IsPassableThrough; }
    }
}
