/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour {

    private static int mChildCount;

	void Awake () {
        mChildCount = GetComponentsInChildren<KidInteractable>().Length;
	}
	
    public static int ChildCount
    {
        get { return mChildCount; }
    }

}
