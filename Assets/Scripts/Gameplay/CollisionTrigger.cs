/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour {

	[SerializeField]
	private EGameTrigger eventToTrigger = EGameTrigger.LEVEL_END_REACHED;

	public void OnTriggerEnter2D(Collider2D pOther)
	{
        if (pOther.GetComponent<MainCharacterController>() != null)
        {
			EventService.DispatchEvent(eventToTrigger);
		}
	}
}
