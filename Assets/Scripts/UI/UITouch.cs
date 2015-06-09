/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

//TODO: Find a better name
public class UITouch : MonoBehaviour {
	void Start () {
        gameObject.SetActive(Input.touchSupported);
	}
}
