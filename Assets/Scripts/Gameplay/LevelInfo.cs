/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour {

    [SerializeField]
    private int m_StartLifeBalloonCount;

    private static int mChildCount;
    private static int mStaticStartLifeBalloonCount;

	void Awake () {
        mChildCount = GetComponentsInChildren<KidInteractable>().Length;
        mStaticStartLifeBalloonCount = m_StartLifeBalloonCount;
	}
	
    public static int ChildCount
    {
        get { return mChildCount; }
    }

    public static int StartLifeBalloonCount
   {
       get { return mStaticStartLifeBalloonCount; }
   }

}
