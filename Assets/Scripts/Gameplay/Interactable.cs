using UnityEngine;
using System.Collections;
using Radix.Event;

public class Interactable : MonoBehaviour {

    [SerializeField]
    private EGameEvent m_GameEvent;

    [SerializeField]
    private bool m_IsPassableThrough = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DispacthEvent()
    {
        EventService.DipatchEvent(m_GameEvent, this);
    }

    public bool IsPassableThrough
    {
        get { return m_IsPassableThrough; }
    }
}
