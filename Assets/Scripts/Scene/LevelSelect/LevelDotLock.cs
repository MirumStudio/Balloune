﻿/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class LevelDotLock : MonoBehaviour {

	public void UpdateLockVisibility(bool aLockVisibility) 
    {
        gameObject.SetActive(aLockVisibility);
	}
}
