/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Service;
using UnityEngine;

public class BaseView : MonoBehaviour {

    static public string SCENE_NAME = "NONE";

    virtual protected void Awake()
    {
		//TODO: create a class Radix for Radix.Init or something like that
        ServiceManager.Instance.Init();
    }
}
