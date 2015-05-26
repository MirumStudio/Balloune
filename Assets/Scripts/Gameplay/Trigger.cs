using UnityEngine;
using System.Collections;
using Radix.Event;

public class Trigger : MonoBehaviour {

	[SerializeField]
	private EGameTrigger eventToTrigger = EGameTrigger.LEVEL_END_REACHED;

	void Start () {
	
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<MainCharacterController>() != null) {
			EventService.DispatchEvent(eventToTrigger, null);
		}
	}
}
